using SDQRealEstate.Core.Application.Enums;
using SDQRealEstate.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDQRealEstate.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace SDQRealEstate.Infrastructure.Identity.Seeds
{
    public class DefaultClientUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();
            defaultUser.FirstName = "Onell";
            defaultUser.LastName = "Dishmey";
            defaultUser.UserName = "Onell.Dishmey";
            defaultUser.Email = "202010179@itla.edu.do";
            defaultUser.PhoneNumber = "8295412235";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;
            defaultUser.Foto = "/Images/Users/Default/onell.jpg";

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Onell00+");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Cliente.ToString());

                }

            }
        }

    }
}
