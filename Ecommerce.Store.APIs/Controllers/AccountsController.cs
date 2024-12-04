using AutoMapper;
using Ecommerce.Store.APIs.Errors;
using Ecommerce.Store.APIs.Extentions;
using Ecommerce.Store.Core.Dtos.Auth;
using Ecommerce.Store.Core.Entities.Identity;
using Ecommerce.Store.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Store.APIs.Controllers
{

    public class AccountsController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AccountsController(
            IUserService userService,
            ITokenService tokenService,
            IMapper mapper,
            UserManager<AppUser> userManager)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
            _userManager = userManager;
        }
        //

        [HttpPost("login")] // POST : /api/Accounts/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userService.LoginAsync(loginDto);
            if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            return Ok(user);
        }
        //


        [HttpPost("register")] // POST : /api/Accounts/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userService.RegisterAsync(registerDto);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid Registration !!"));
            return Ok(user);
        }
        //

        [Authorize]
        [HttpGet("GetCurrentUser")] // GET : /api/Accounts/GetCurrentUser
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }
        //

        [Authorize]
        [HttpGet("Address")] // GET : /api/Accounts/Address
        public async Task<ActionResult<UserDto>> GetCurrentUserAddress()
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);

            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(_mapper.Map<AddressDto>(user.Address));
        }
        //


    }
}
