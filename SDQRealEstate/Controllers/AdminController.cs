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
using SDQRealEstate.Core.Application.ViewModels.TipoPropiedad;
using AutoMapper;
using SDQRealEstate.Core.Application.ViewModels.TipoVenta;
using SDQRealEstate.Core.Application.ViewModels.Mejoras;

namespace WebApp.SDQRealEstate.Controllers  
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITipoPropiedadService _itipoPropiedadService;
        private readonly IManageUsersService _manageuserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationResponse? _userLogged;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPropiedadService _ipropiedadService;
        private readonly IMapper _mapper;
        private readonly ITipoVentaService _itipoVentaService;
        private readonly IMejoraService _imejoraService;

        public AdminController(IUserService userService, IMejoraService imejoraService,ITipoVentaService itipoVentaService,IMapper mapper,
            UserManager<ApplicationUser> userManager, IPropiedadService ipropiedadService ,ITipoPropiedadService itipoPropiedadService, 
            IManageUsersService manageuserService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
            _itipoPropiedadService = itipoPropiedadService;
            _manageuserService = manageuserService;
            _itipoVentaService = itipoVentaService;
            _httpContextAccessor = httpContextAccessor;
            _ipropiedadService = ipropiedadService;
            _imejoraService = imejoraService;
            _userLogged = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }


        public IActionResult Index()
        {
            return View();
        }

        #region AGENTES
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
            ViewBag.currentuser = _userLogged.Id;

            return View();
        }
        #endregion

        #region Administradores
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
        #endregion

        #region Desarrolladores
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
        #endregion

        #region Propiedades
        public async Task<IActionResult> propiedades()
        {
            var temp  = await _itipoPropiedadService.GetAllViewModel();
            if (temp != null && temp.Count > 0)
            {
                foreach (TipoPropiedadViewModel i in temp)
                {
                    i.Cantidad = await _ipropiedadService.GetCantidadTipoPropiedad(i.Id);
                }
            }
                return View(temp);
        }

        public IActionResult CreateTipoPropiedad()
        {            
            return View(new SaveTipoPropiedadViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTipoPropiedad(SaveTipoPropiedadViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                return View(sv);
            }

            var usertemp =  await _itipoPropiedadService.Add(sv);
            if (usertemp != null)
            {
                return RedirectToRoute(new { controller = "Admin", action = "propiedades" });
            }

            return View(sv);

        }
        public async Task<IActionResult> EditarTipoPropiedad(int id)
        {
            var usertemp = await _itipoPropiedadService.GetByIdSaveViewModel(id);
            return View("CreateTipoPropiedad", usertemp);

        }

        [HttpPost]
        public async Task<IActionResult> EditarTipoPropiedad(SaveTipoPropiedadViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                return View(sv);

            }
            await _itipoPropiedadService.Update(sv, sv.Id);
            return RedirectToRoute(new { controller = "Admin", action = "propiedades" });

        }

        public async Task<IActionResult> EliminarTipoPropiedad(int id)
        {

            SaveTipoPropiedadViewModel us = await _itipoPropiedadService.GetByIdSaveViewModel(id);
            return View(us);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarTipoPropiedad(TipoPropiedadViewModel us)
        {
            var usertemp = await _itipoPropiedadService.GetByIdSaveViewModel(us.Id);
            await _itipoPropiedadService.Delete(us.Id);

            return RedirectToRoute(new { controller = "Admin", action = "propiedades" });
        }
        #endregion

        #region Tipo Ventas
        public async Task<IActionResult> Ventas()
        {
            var temp = await _itipoVentaService.GetAllViewModel();
            if (temp != null && temp.Count > 0)
            {
                foreach (TipoVentaViewModel i in temp)
                {
                    i.Cantidad = await _ipropiedadService.GetCantidadTipoVenta(i.Id);
                }
            }
            return View(temp);
        }

        public IActionResult CreateTipoVenta()
        {
            return View(new SaveTipoVentaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTipoVenta(SaveTipoVentaViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                return View(sv);
            }

            var usertemp = await _itipoVentaService.Add(sv);
            if (usertemp != null)
            {
                return RedirectToRoute(new { controller = "Admin", action = "Ventas" });
            }

            return View(sv);

        }
        public async Task<IActionResult> EditarTipoVenta(int id)
        {
            var usertemp = await _itipoVentaService.GetByIdSaveViewModel(id);
            return View("CreateTipoVenta", usertemp);

        }

        [HttpPost]
        public async Task<IActionResult> EditarTipoVenta(SaveTipoVentaViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                return View(sv);

            }
            await _itipoVentaService.Update(sv, sv.Id);
            return RedirectToRoute(new { controller = "Admin", action = "Ventas" });

        }

        public async Task<IActionResult> EliminarTipoVenta(int id)
        {
            SaveTipoPropiedadViewModel us = await _itipoVentaService.GetByIdSaveViewModel(id);
            return View(us);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarTipoVenta(TipoPropiedadViewModel us)
        {
            var usertemp = await _itipoVentaService.GetByIdSaveViewModel(us.Id);
            await _itipoVentaService.Delete(us.Id);

            return RedirectToRoute(new { controller = "Admin", action = "Ventas" });
        }

        #endregion

        #region Mejoras
        public async Task<IActionResult> Mejora()
        {
            var temp = await _imejoraService.GetAllViewModel();
            if (temp != null && temp.Count > 0)
            {
                foreach (MejoraViewModel i in temp)
                {
                    i.Cantidad = await _ipropiedadService.GetCantidadMejora(i.Id);
                }
            }
            return View(temp);
        }

        public IActionResult CreateMejora()
        {
            return View(new SaveMejoraViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateMejora(SaveMejoraViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                return View(sv);
            }

            var usertemp = await _imejoraService.Add(sv);
            if (usertemp != null)
            {
                return RedirectToRoute(new { controller = "Admin", action = "Mejora" });
            }

            return View(sv);

        }
        public async Task<IActionResult> EditarMejora(int id)
        {
            var usertemp = await _imejoraService.GetByIdSaveViewModel(id);
            return View("CreateMejora", usertemp);
        }

        [HttpPost]
        public async Task<IActionResult> EditarMejora(SaveTipoVentaViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                return View(sv);

            }
            await _imejoraService.Update(sv, sv.Id);
            return RedirectToRoute(new { controller = "Admin", action = "Mejora" });

        }

        public async Task<IActionResult> EliminarMejora(int id)
        {
            SaveMejoraViewModel us = await _imejoraService.GetByIdSaveViewModel(id);
            return View(us);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarMejora(TipoPropiedadViewModel us)
        {
            var usertemp = await _imejoraService.GetByIdSaveViewModel(us.Id);
            await _imejoraService.Delete(us.Id);

            return RedirectToRoute(new { controller = "Admin", action = "Mejora" });
        }

        #endregion
        
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}

