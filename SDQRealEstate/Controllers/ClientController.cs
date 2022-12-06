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
        private readonly ITipoVentaService _itipoVentaService;
        private readonly ITipoPropiedadService _itipoPropiedadService;
        private readonly IMejoraService _imejoraService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFotosService _ifotosService;
        private readonly IMapper _mapper;

        public ClientController(IUserService userService, IFotosService ifotosService, IMejoraService imejoraService, ITipoVentaService itipoVentaService, IMapper mapper,
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

        public IActionResult AccessDenied()
        {
            return View();
        }

       
    }
}

