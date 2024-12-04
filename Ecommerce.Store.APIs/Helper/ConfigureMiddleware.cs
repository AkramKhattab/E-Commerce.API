using Ecommerce.Store.APIs.MiddleWares;
using Ecommerce.Store.Repository.Data.Contexts;
using Ecommerce.Store.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Store.Repository.Identity.Contexts;
using Ecommerce.Store.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Ecommerce.Store.Core.Entities.Identity;

namespace Ecommerce.Store.APIs.Helper
{
    public static class ConfigureMiddleware
    {
        public static async Task<WebApplication> ConfigureMiddlewaresAsync(this WebApplication app)
        {
            //
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<EcommerceDbContext>(); // ASK CLR Create Object EcommerceDbContext
            var identityDbContext = services.GetRequiredService<StoreIdentityDbContext>(); // ASK CLR Create Object StoreIdentityDbContext
            var userManager = services.GetRequiredService<UserManager<AppUser>>(); // ASK CLR Create Object StoreIdentityDbContext
            var loggerFactory = services.GetRequiredService<ILoggerFactory>(); // ASK CLR Create Object EcommerceDbContext

            try
            {
                await context.Database.MigrateAsync(); //Update-Database

                await EcommerceDbContextSeed.SeedAsync(context);

                await identityDbContext.Database.MigrateAsync();

                await StoreIdentityDbContextSeed.SeedAppUserAsync(userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex, "There is some problems during apply migrations !");
            }
            //


            app.UseMiddleware<ExceptionMiddleWare>(); // Configure User-Defined [ExceptionMiddleWare] MiddleWare

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseAuthorization();


            app.MapControllers();

            return app;

        }
    }
}
