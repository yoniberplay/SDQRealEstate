using Microsoft.AspNetCore.Mvc;
using SDQRealEstate.Core.Application.ViewModels.User;
using System.Threading.Tasks;
using SDQRealEstate.Core.Application.Helpers;
using SDQRealEstate.Core.Application.Interfaces.Services;
using SDQRealEstate.Core.Application.Dtos.Account;
using WebApp.SDQRealEstate.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SDQRealEstate.Infrastructure.Identity.Entities;
using AutoMapper;
using SDQRealEstate.Core.Application.ViewModels.Propiedad;
using SDQRealEstate.Core.Application.ViewModels.Favorita;

namespace WebApp.SDQRealEstate.Controllers  
{
    [Authorize(Roles = "Cliente")]
    public class ClientController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly AuthenticationResponse? _userLogged;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPropiedadService _ipropiedadService;
        private readonly IFavoritaService _ifavoritaService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITipoVentaService _itipoVentaService;
        private readonly ITipoPropiedadService _itipoPropiedadService;
        private readonly IMejoraService _imejoraService;
        private readonly IFotosService _ifotosService;

        public ClientController(IUserService userService, IFotosService ifotosService, IFavoritaService ifavoritaService, 
            IMejoraService imejoraService, ITipoVentaService itipoVentaService, IMapper mapper,
            IPropiedadService ipropiedadService, UserManager<ApplicationUser> userManager, ITipoPropiedadService itipoPropiedadService,
            IManageUsersService manageuserService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userLogged = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _ipropiedadService = ipropiedadService;
            _ifavoritaService = ifavoritaService;
            _ifotosService = ifotosService;
            _itipoPropiedadService = itipoPropiedadService;
            _itipoVentaService= itipoVentaService;
            _imejoraService= imejoraService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.TipoPropiedad = await _itipoPropiedadService.GetAllViewModel();
            ViewBag.Venta = await _itipoVentaService.GetAllViewModel();
            ViewBag.Mejoras = await _imejoraService.GetAllViewModel();
            ViewBag.listapropiedades = await _ipropiedadService.GetAllViewModelIcnlude();
            return View(new FilterPropiedad());

        }

        public async Task<IActionResult> Marcar(int Id)
        {
            String userid = _userLogged.Id;
            var list = await _ifavoritaService.GetAllViewModel();
            var temp = list.Where(L => L.IdPropiedad == Id && string.Equals(L.IdUser, userid, StringComparison.OrdinalIgnoreCase)).ToList();

            if (temp==null || temp.Count == 0)
            {
                SaveFavoritaViewModel sav = new();
                sav.IdPropiedad = Id;
                sav.IdUser = userid;
                await _ifavoritaService.Add(sav);
            }

            ViewBag.TipoPropiedad = await _itipoPropiedadService.GetAllViewModel();
            ViewBag.Venta = await _itipoVentaService.GetAllViewModel();
            ViewBag.Mejoras = await _imejoraService.GetAllViewModel();
            ViewBag.listapropiedades = await _ipropiedadService.GetAllViewModelIcnlude();
            return View("Index", new FilterPropiedad());
        }

        public async Task<IActionResult> DesmarcarAsync(int Id)
        {
            String userid = _userLogged.Id;
            var list = await _ifavoritaService.GetAllViewModel();
            var temp = list.Where(L => L.IdPropiedad == Id && string.Equals(L.IdUser, userid, StringComparison.OrdinalIgnoreCase)).ToList().FirstOrDefault();
            await _ifavoritaService.Delete(temp.Id);


            ViewBag.TipoPropiedad = await _itipoPropiedadService.GetAllViewModel();
            ViewBag.Venta = await _itipoVentaService.GetAllViewModel();
            ViewBag.Mejoras = await _imejoraService.GetAllViewModel();
            ViewBag.listapropiedades = await _ipropiedadService.GetAllViewModelIcnlude();
            return View("Index", new FilterPropiedad());
        }

        public async Task<IActionResult> MisPropiedadesAsync()
        {
            String userid = _userLogged.Id;
            var list = await _ifavoritaService.GetAllWithProperties();
            var favoritelist = list.Where(L => string.Equals(L.IdUser, userid, StringComparison.OrdinalIgnoreCase)).ToList();

            var allpropiedad = await _ipropiedadService.GetAllViewModelIcnlude();

            List<PropiedadViewModel> lista = new List<PropiedadViewModel>();
            foreach (var item in allpropiedad)
            {
                foreach (var z in favoritelist)
                {
                    if (item.Id == z.IdPropiedad)
                    {
                        lista.Add(item);
                    }
                }
            }
            ViewBag.listapropiedades = lista;


            return View();
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

       
    }
}

