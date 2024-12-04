using Ecommerce.Store.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce.Store.APIs.Extentions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null);

            var user = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == userEmail);

            if (user is null) return null;

            return user;
        }
    }
}
