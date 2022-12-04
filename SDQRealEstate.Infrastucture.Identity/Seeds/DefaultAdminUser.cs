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
    public class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();
            defaultUser.FirstName = "YONIBER";
            defaultUser.LastName = "ENCARNACION NUÑEZ";
            defaultUser.UserName = "root";
            defaultUser.Email = "20211442@itla.edu.do";
            defaultUser.PhoneNumber = "8299884791";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;
            defaultUser.Foto = "/Images/Users/Default/yoniber.jpg";

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser,"YOniber00+");
                    await userManager.AddToRoleAsync(defaultUser,Roles.Cliente.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Agente.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());

                }

            }
        }

    }
}
