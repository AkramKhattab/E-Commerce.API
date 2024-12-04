using Ecommerce.Store.Core.Services.Contract;
using Ecommerce.Store.Core;
using Ecommerce.Store.Repository;
using Ecommerce.Store.Repository.Data.Contexts;
using Ecommerce.Store.Service.Services.Products;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Store.Core.Mapping.Products;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Store.APIs.Errors;
using Ecommerce.Store.Core.Repositories.Contract;
using Ecommerce.Store.Repository.Repositories;
using StackExchange.Redis;
using Ecommerce.Store.Core.Mapping.Baskets;
using Ecommerce.Store.Repository.Identity.Contexts;
using Ecommerce.Store.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Ecommerce.Store.Service.Services.Caches;
using Ecommerce.Store.Service.Services.Tokens;
using Ecommerce.Store.Service.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ecommerce.Store.Core.Mapping.Auth;
using Ecommerce.Store.Core.Mapping.Orders;
using Ecommerce.Store.Service.Services.Orders;
using Ecommerce.Store.Service.Services.Baskets;
using Ecommerce.Store.Service.Services.Payments;

namespace Ecommerce.Store.APIs.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            // Good: Centralized method for adding multiple services
            services.AddBuiltInService();           // Adds basic MVC controllers
            services.AddSwaggerService();          // Configures Swagger for API documentation
            services.AddDbContextService(configuration);  // Sets up database contexts
            services.AddUserDefinedService();      // Registers custom services and repositories
            services.AddAutoMapperService(configuration);  // Configures AutoMapper profiles
            services.ConfigureInvalidModelStateResponseService();  // Customizes model validation response
            services.AddRedisService(configuration);  // Adds Redis connection
            services.AddIdentityService();         // Configures ASP.NET Core Identity
            services.AddAuthenticationService(configuration);  // Configures JWT authentication

            return services;
        }
        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers();



            return services;
        }
        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            return services;
        }
        private static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EcommerceDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                 sqlOptions => {
                     // Add connection resilience
                     sqlOptions.EnableRetryOnFailure(
                         maxRetryCount: 5,  // Number of retry attempts
                         maxRetryDelay: TimeSpan.FromSeconds(30),  // Max delay between retries
                         errorNumbersToAdd: null  // Specific error numbers to retry
                         );
                });
            });

            services.AddDbContext<StoreIdentityDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });
            return services;
        }
        private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }
        private static IServiceCollection AddAutoMapperService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            services.AddAutoMapper(M => M.AddProfile(new AuthProfile()));
            services.AddAutoMapper(M => M.AddProfile(new OrderProfile(configuration)));
            return services;
        }
        private static IServiceCollection ConfigureInvalidModelStateResponseService(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                             .SelectMany(P => P.Value.Errors)
                                             .Select(E => E.ErrorMessage)
                                             .ToArray();


                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            }); return services;
        }
        private static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>
            {
                try
                {
                    var connection = configuration.GetConnectionString("Redis");

                    // Add more robust connection configuration
                    var redisConfiguration = ConfigurationOptions.Parse(connection);
                    redisConfiguration.ConnectTimeout = 5000;  // 5 seconds
                    redisConfiguration.ConnectRetry = 3;       // Retry 3 times

                    return ConnectionMultiplexer.Connect(redisConfiguration);
                }
                catch (Exception ex)
                {
                    // Log the error or handle it appropriately
                    Console.WriteLine($"Redis Connection Error: {ex.Message}");
                    throw;  // Or return a default/fallback implementation
                }
            });

            return services;
        }
        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                // Password configuration
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;

                // User settings
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<StoreIdentityDbContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);

            return services;
        }
        private static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),

                    // Add clock skew tolerance
                    ClockSkew = TimeSpan.FromMinutes(5)
                };

                // Optional: Add event handling for additional logging or custom validation
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        // Optional additional validation logic
                        // For example, check if user still exists or has required roles
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        // Log authentication failures
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

    }
}
