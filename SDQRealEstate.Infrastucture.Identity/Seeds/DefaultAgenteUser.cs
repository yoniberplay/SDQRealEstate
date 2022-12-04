using SDQRealEstate.Core.Application.Enums;
using SDQRealEstate.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Infrastructure.Identity.Seeds
{
    public class DefaultAgenteUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();
            defaultUser.FirstName = "Ramy";
            defaultUser.LastName = "Campusano Volquez";
            defaultUser.UserName = "Ramy.Campusano";
            defaultUser.Email = "202010153@itla.edu.do";
            defaultUser.PhoneNumber = "8097978451";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;
            defaultUser.Foto = "/Images/Users/Default/ramy.jpg";

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Campusano00+");
                    await userManager.AddToRoleAsync(defaultUser,Roles.Agente.ToString());
                }

            }
        }

    }
}
