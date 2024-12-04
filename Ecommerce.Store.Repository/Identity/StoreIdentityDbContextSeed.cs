using Ecommerce.Store.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Repository.Identity
{
    public static class StoreIdentityDbContextSeed
    {
        public async static Task SeedAppUserAsync(UserManager<AppUser>_userManager)
        {
            if(_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    Email = "akramtest1994@gmail.com",
                    DisplayName = "Akram Khattab",
                    UserName = "akram.khattab",
                    PhoneNumber = "01234567890",
                    Address = new Address()
                    {
                        FName = "Akram",
                        LName = "Khattab",
                        City = "Cairo",
                        Country = "Egypt",
                        Street = "123-Street",
                    }
                };

                await _userManager.CreateAsync(user, "P@ssW0rd");
            }
        }
    }
}
