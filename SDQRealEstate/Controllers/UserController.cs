using Microsoft.AspNetCore.Mvc;
using SDQRealEstate.Core.Application.ViewModels.User;
using System.Threading.Tasks;
using SDQRealEstate.Core.Application.Helpers;
using SDQRealEstate.Core.Application.Interfaces.Services;
using SDQRealEstate.Core.Application.Dtos.Account;
using WebApp.SDQRealEstate.Middlewares;  

namespace WebApp.SDQRealEstate.Controllers  
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);


                var Desarrollador = User != null ? userVm.Roles.Any(r => r == "Desarrollador") : false;
                if (Desarrollador)
                {//no tiene permiso para usar la web app.
                    HttpContext.Session.Remove("user");
                    return RedirectToRoute(new { controller = "Home", action = "NoPermiso" });
                }

                var isAdmin = User != null ? userVm.Roles.Any(r => r == "Admin") : false;
                var isClient = User != null ? userVm.Roles.Any(r => r == "Cliente") : false;
                var isAgent = User != null ? userVm.Roles.Any(r => r == "Agente") : false;

                String controlador = "";
                if (isAdmin)
                {
                    controlador = "Admin";
                }
                else if (isClient)
                {
                    controlador = "Client";
                }
                else if (isAgent)
                {
                    controlador = "Agent";

                }

                return RedirectToRoute(new { controller = controlador, action = "Index" });

            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }


        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }


        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];
            ForgotPasswordResponse response = await _userService.ForgotPasswordAsync(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel { Token = token });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            
            ResetPasswordResponse response = await _userService.ResetPasswordAsync(vm);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Create()
        {
            return View(new SaveUserViewModel());
        }


        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Create(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.HasError = true;
                return View(vm);
            }

            var origin = Request.Headers["origin"];
            await _userService.RegisterAsync(vm, origin);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}

