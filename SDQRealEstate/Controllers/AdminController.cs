using Microsoft.AspNetCore.Mvc;
using SDQRealEstate.Core.Application.ViewModels.User;
using System.Threading.Tasks;
using SDQRealEstate.Core.Application.Helpers;
using SDQRealEstate.Core.Application.Interfaces.Services;
using SDQRealEstate.Core.Application.Dtos.Account;
using WebApp.SDQRealEstate.Middlewares;
using Microsoft.AspNetCore.Authorization;
using SDQRealEstate.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace WebApp.SDQRealEstate.Controllers  
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IManageUsersService _manageuserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationResponse _userLogged;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminController(IUserService userService, UserManager<ApplicationUser> userManager, IManageUsersService manageuserService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _userManager = userManager;
            _manageuserService = manageuserService;
            _httpContextAccessor = httpContextAccessor;
            _userLogged = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DESACTIVARagentes(String id)
        {
            var usertemp = await _userManager.FindByIdAsync(id);
            usertemp.EmailConfirmed=false;
            await _userManager.UpdateAsync(usertemp);
            return RedirectToRoute(new { controller = "Admin", action = "agentes" });
        }
        public async Task<IActionResult> ACTIVARagentes(String id)
        {
            var usertemp = await _userManager.FindByIdAsync(id);
            usertemp.EmailConfirmed = true;
            await _userManager.UpdateAsync(usertemp);
            return RedirectToRoute(new { controller = "Admin", action = "agentes" });
        }

        public async Task<IActionResult> EliminarAgente(String id)
        {
            var usertemp = await _userManager.FindByIdAsync(id);
            UserViewModel us = new();
            us.FirstName = usertemp.FirstName;
            us.LastName = usertemp.LastName;
            us.Email = usertemp.Email;
            us.Id = usertemp.Id;
            us.Email=usertemp.Email;
            us.Phone = usertemp.PhoneNumber;
            us.Username = usertemp.UserName;
            return View(us);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarAgente(UserViewModel us)
        {
            var usertemp = await _userManager.FindByIdAsync(us.Id);
            await _userManager.DeleteAsync(usertemp);

            return RedirectToRoute(new { controller = "Admin", action = "agentes" });
        }
        public async Task<IActionResult> agentesAsync()
        {
            ViewBag.UsersAgent = await _manageuserService.GetbyRolList("Agente");

            return View();
        }

        public async Task<IActionResult> Administradores()
        {
            ViewBag.UsersAdmin = await _manageuserService.GetbyRolList("Admin");
            ViewBag.currentuser = _userLogged.Id;
            return View();
        }

        public async Task<IActionResult> DesactivarAdmin(String id)
        {
            var usertemp = await _userManager.FindByIdAsync(id);
            UserViewModel us = new();
            us.FirstName = usertemp.FirstName;
            us.LastName = usertemp.LastName;
            us.Email = usertemp.Email;
            us.Id = usertemp.Id;
            us.Email = usertemp.Email;
            us.Phone = usertemp.PhoneNumber;
            us.Username = usertemp.UserName;

            return View(us);
        }
        [HttpPost]
        public async Task<IActionResult> DesactivarAdmin(UserViewModel us)
        {
            var usertemp = await _userManager.FindByIdAsync(us.Id);
            usertemp.EmailConfirmed = false;
            await _userManager.UpdateAsync(usertemp);
            return RedirectToRoute(new { controller = "Admin", action = "Administradores" });
        }
        public async Task<IActionResult> ActivarAdmin(String id)
        {
            var usertemp = await _userManager.FindByIdAsync(id);
            usertemp.EmailConfirmed = true;
            await _userManager.UpdateAsync(usertemp);
            return RedirectToRoute(new { controller = "Admin", action = "Administradores" });
        }

        public IActionResult CreateAdmin()
        {
            return View(new SaveUserViewModel());
        }

        public async Task<IActionResult> UpdateAdmin(String id)
        {
            var usertemp = await _userManager.FindByIdAsync(id);

            SaveUserViewModel us = new();
            us.FirstName = usertemp.FirstName;
            us.LastName = usertemp.LastName;
            us.Email = usertemp.Email;
            us.Id = usertemp.Id;
            us.Email = usertemp.Email;
            us.Phone = usertemp.PhoneNumber;
            us.Username = usertemp.UserName;

            return View("CreateAdmin",us);

        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin(SaveUserViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                sv.HasError = true;
                return View(sv);
            }

            var origin = Request.Headers["origin"];
            await _userService.RegisterAsync(sv, origin);

            var usertemp = await _userManager.FindByEmailAsync(sv.Email);
            usertemp.EmailConfirmed = true;
            await _userManager.UpdateAsync(usertemp);

            return RedirectToRoute(new { controller = "Admin", action = "Administradores" });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAdmin(SaveUserViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                sv.HasError = true;
                return View(sv);

            }

            var usertemp = await _userManager.FindByIdAsync(sv.Id);

            usertemp.FirstName = sv.FirstName;
            usertemp.LastName = sv.LastName;
            usertemp.PasswordHash = sv.Password;
            usertemp.Email = sv.Email;
            usertemp.PhoneNumber = sv.Phone;
            usertemp.UserName = sv.Username;
            usertemp.Foto = AdmFiles.UploadFile(sv.File, usertemp.Id, "Users");

            await _userManager.UpdateAsync(usertemp);


            return RedirectToRoute(new { controller = "Admin", action = "Administradores" });

        }


        public async Task<IActionResult> Desarrolladores()
        {
            ViewBag.UsersDesarrollador = await _manageuserService.GetbyRolList("Desarrollador");
            ViewBag.currentuser = _userLogged.Id;

            return View();
        }

        public IActionResult CreateDesarrollador()
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateDesarrollador(SaveUserViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                sv.HasError = true;
                return View(sv);
            }

            var origin = Request.Headers["origin"];
            await _userService.RegisterAsync(sv, origin);

            var usertemp = await _userManager.FindByEmailAsync(sv.Email);
            usertemp.EmailConfirmed = true;
            await _userManager.UpdateAsync(usertemp);

            return RedirectToRoute(new { controller = "Admin", action = "Desarrolladores" });
        }

        public async Task<IActionResult> UpdateDesarrollador(String id)
        {
            var usertemp = await _userManager.FindByIdAsync(id);

            SaveUserViewModel us = new();
            us.FirstName = usertemp.FirstName;
            us.LastName = usertemp.LastName;
            us.Email = usertemp.Email;
            us.Id = usertemp.Id;
            us.Email = usertemp.Email;
            us.Phone = usertemp.PhoneNumber;
            us.Username = usertemp.UserName;

            return View("CreateDesarrollador", us);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateDesarrollador(SaveUserViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                sv.HasError = true;
                return View(sv);

            }

            var usertemp = await _userManager.FindByIdAsync(sv.Id);

            usertemp.FirstName = sv.FirstName;
            usertemp.LastName = sv.LastName;
            usertemp.PasswordHash = sv.Password;
            usertemp.Email = sv.Email;
            usertemp.PhoneNumber = sv.Phone;
            usertemp.UserName = sv.Username;
            usertemp.Foto = AdmFiles.UploadFile(sv.File, usertemp.Id, "Users");

            await _userManager.UpdateAsync(usertemp);


            return RedirectToRoute(new { controller = "Admin", action = "Desarrolladores" });

        }

        public async Task<IActionResult> DesactivarDesarrollador(String id)
        {
            var usertemp = await _userManager.FindByIdAsync(id);
            UserViewModel us = new();
            us.FirstName = usertemp.FirstName;
            us.LastName = usertemp.LastName;
            us.Email = usertemp.Email;
            us.Id = usertemp.Id;
            us.Email = usertemp.Email;
            us.Phone = usertemp.PhoneNumber;
            us.Username = usertemp.UserName;

            return View(us);
        }
        [HttpPost]
        public async Task<IActionResult> DesactivarDesarrollador(UserViewModel us)
        {
            var usertemp = await _userManager.FindByIdAsync(us.Id);
            usertemp.EmailConfirmed = false;
            await _userManager.UpdateAsync(usertemp);
            return RedirectToRoute(new { controller = "Admin", action = "Desarrolladores" });
        }
        public async Task<IActionResult> ActivarDesarrollador(String id)
        {
            var usertemp = await _userManager.FindByIdAsync(id);
            usertemp.EmailConfirmed = true;
            await _userManager.UpdateAsync(usertemp);
            return RedirectToRoute(new { controller = "Admin", action = "Desarrolladores" });
        }


        public IActionResult propiedades()
        {
            return View();
        }

        public IActionResult ventas()
        {
            return View();
        }

        public IActionResult mejoras()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}

