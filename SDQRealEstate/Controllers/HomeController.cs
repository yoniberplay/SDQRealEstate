using Microsoft.AspNetCore.Mvc;
using SDQRealEstate.Core.Application.ViewModels.Mejoras;
using SDQRealEstate.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SDQRealEstate.Core.Application.ViewModels.Propiedad;
using System.Threading.Tasks;
using SDQRealEstate.Core.Application.Helpers;
using SDQRealEstate.Core.Application.Interfaces.Services;
using SDQRealEstate.Core.Application.Dtos.Account;
using WebApp.SDQRealEstate.Middlewares;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using SDQRealEstate.Core.Application.ViewModels.Fotos;
using Microsoft.AspNetCore.Identity;
using SDQRealEstate.Infrastructure.Identity.Entities;
using SDQRealEstate.Core.Application.ViewModels.User;

namespace WebApp.SDQRealEstate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly AuthenticationResponse? _userLogged;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPropiedadService _ipropiedadService;
        private readonly ITipoVentaService _itipoVentaService;
        private readonly ITipoPropiedadService _itipoPropiedadService;
        private readonly IMejoraService _imejoraService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFotosService _ifotosService;
        private readonly IMapper _mapper;

        public HomeController(IUserService userService, IFotosService ifotosService, IMejoraService imejoraService, ITipoVentaService itipoVentaService, IMapper mapper,
            IPropiedadService ipropiedadService, UserManager<ApplicationUser> userManager, ITipoPropiedadService itipoPropiedadService,
            IManageUsersService manageuserService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userLogged = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _ipropiedadService = ipropiedadService;
            _itipoPropiedadService = itipoPropiedadService;
            _itipoVentaService = itipoVentaService;
            _imejoraService = imejoraService;
            _ifotosService = ifotosService;

        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.TipoPropiedad = await _itipoPropiedadService.GetAllViewModel();
            ViewBag.Venta = await _itipoVentaService.GetAllViewModel();
            ViewBag.Mejoras = await _imejoraService.GetAllViewModel();
            ViewBag.listapropiedades = await _ipropiedadService.GetAllViewModelIcnlude();
            return View(new FilterPropiedad());
        }

        public async Task<IActionResult> AgentesAsync()
        {
            var temp = await _userManager.GetUsersInRoleAsync("Agente");
            ViewBag.ListaAgentes = temp.OrderBy(x => x.FirstName).Where(x => x.EmailConfirmed == true).ToList();
            return View(new FilterByNameViewModel());
        }

        public async Task<IActionResult> PropiedadesAgente(String id)
        {
            var temp = await _ipropiedadService.GetAllViewModelIcnlude();
            ViewBag.listapropiedades = temp.ToList().Where(x => x.UserId == id).ToList();
            var usertemp  = _userManager.Users.Where(i => i.Id == id).ToList().FirstOrDefault();
            ViewBag.agente = usertemp;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> BusquedaByName(FilterByNameViewModel fp)
        {
            var temp = await _userManager.GetUsersInRoleAsync("Agente");
            if (!ModelState.IsValid)
            {
                ViewBag.ListaAgentes = temp.OrderBy(x => x.FirstName).Where(x => x.EmailConfirmed == true).ToList();
                return View("Agentes", fp);
            }
            ViewBag.ListaAgentes = temp.Where(L => string.Equals(L.FirstName, fp.Name, StringComparison.OrdinalIgnoreCase))
                .Where(x => x.EmailConfirmed == true).ToList();

            return View("Agentes", fp);
        }

        [HttpPost]
        public async Task<IActionResult> Busqueda(FilterPropiedad fp)
        {
            ViewBag.TipoPropiedad = await _itipoPropiedadService.GetAllViewModel();
            ViewBag.Venta = await _itipoVentaService.GetAllViewModel();
            ViewBag.Mejoras = await _imejoraService.GetAllViewModel();
            var temp = await _ipropiedadService.GetAllViewModelIcnlude();

            if (fp.Codigo != 0)
            {
                temp = temp.Where(x => x.Codigo == fp.Codigo).ToList();
            }
            if (fp.TipoPropiedadId != 0)
            {
                temp = temp.Where(x => x.tipoPropiedades.Id == fp.TipoPropiedadId).ToList();
            }
            if (fp.CantBanos != 0)
            {
                temp = temp.Where(x => x.CantBanos == fp.CantBanos).ToList();
            }
            if (fp.CantHabitaciones != 0)
            {
                temp = temp.Where(x => x.CantHabitaciones == fp.CantHabitaciones).ToList();
            }
            if (fp.PrecioMax != 0)
            {
                temp = temp.Where(x => x.Precio <= fp.PrecioMax).ToList();
            }
            if (fp.PrecioMin != 0)
            {
                temp = temp.Where(x => x.Precio >= fp.PrecioMin).ToList();
            }

            ViewBag.listapropiedades = temp;
            return View("Index",fp);
        }


        public async Task<IActionResult> DetallePropiedad(int id)
        {
            var temp = await _ipropiedadService.GetAllViewModelIcnlude();

            var detalles = temp.ToList().Where(x => x.Id ==id).FirstOrDefault();

            ViewBag.fotos = detalles.fotos.ToArray();

            var usertemp = _userManager.Users.Where(i => i.Id == detalles.UserId).ToList().FirstOrDefault();
            ViewBag.agente = usertemp;

            return View(detalles);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult NoPermiso()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}