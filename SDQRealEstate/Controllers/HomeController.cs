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
            ViewBag.listapropiedades = await _ipropiedadService.GetAllViewModelIcnlude();
            return View();
        }

        public IActionResult Agentes()
        {
            return View();
        }

        public IActionResult Privacy()
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