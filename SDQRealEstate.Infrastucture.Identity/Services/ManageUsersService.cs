using SDQRealEstate.Core.Application.Dtos.Account;
using SDQRealEstate.Core.Application.Enums;
using SDQRealEstate.Core.Application.Interfaces.Services;
using SDQRealEstate.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDQRealEstate.Core.Application.Helpers;
using SDQRealEstate.Core.Application.ViewModels.User;

namespace SDQRealEstate.Infrastructure.Identity.Services
{
    public class ManageUsersService : IManageUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public ManageUsersService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<List<UserViewModel>> GetbyRolList(String rol)
        {
            List<UserViewModel> agentList = new List<UserViewModel>();

            var user = await _userManager.GetUsersInRoleAsync(rol);

            if(user != null)
            {
                foreach (ApplicationUser i in user)
                {
                    agentList.Add(new UserViewModel
                    {
                        FirstName = i.FirstName,
                        LastName = i.LastName,
                        Email = i.Email,
                        EmailConfirmed = i.EmailConfirmed,
                        Foto = i.Foto,
                        Phone = i.PhoneNumber,
                        Username = i.UserName,
                        Id = i.Id
                    });
                }
            }
            

            return agentList;
        }
    }
}
