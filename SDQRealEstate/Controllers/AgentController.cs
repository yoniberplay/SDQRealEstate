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

namespace WebApp.SDQRealEstate.Controllers  
{
    [Authorize(Roles = "Agente")]
    public class AgentController : Controller
    {
        private readonly IUserService _userService;
        private readonly AuthenticationResponse? _userLogged;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPropiedadService _ipropiedadService;
        private readonly ITipoVentaService _itipoVentaService;
        private readonly ITipoPropiedadService _itipoPropiedadService;
        private readonly IMejoraService _imejoraService;
        private readonly IFotosService _ifotosService;
        private readonly IMapper _mapper;

        public AgentController(IUserService userService, IFotosService ifotosService, IMejoraService imejoraService, ITipoVentaService itipoVentaService, IMapper mapper,
            IPropiedadService ipropiedadService, ITipoPropiedadService itipoPropiedadService,
            IManageUsersService manageuserService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _mapper = mapper;
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
            var temp = await _ipropiedadService.GetAllPropiedadByAgentId(_userLogged.Id);
            return View(temp);
        }

        public async Task<IActionResult> MantenimientoPropiedades()
        {
            var temp = await _ipropiedadService.GetAllPropiedadByAgentId(_userLogged.Id);
            return View(temp);
        }
        public async Task<IActionResult> CreatePropiedad()
        {
            ViewBag.TipoPropiedad = await _itipoPropiedadService.GetAllViewModel();
            ViewBag.Venta = await _itipoVentaService.GetAllViewModel();
            ViewBag.Mejoras = await  _imejoraService.GetAllViewModel();

            return View(new SavePropiedadViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePropiedad(SavePropiedadViewModel sv)
        {
            if (!ModelState.IsValid || sv.File == null)
            {
                ViewBag.TipoPropiedad = await _itipoPropiedadService.GetAllViewModel();
                ViewBag.Venta = await _itipoVentaService.GetAllViewModel();
                ViewBag.Mejoras = await _imejoraService.GetAllViewModel();
                return View(sv);
            }

            sv.UserId = _userLogged.Id;
            sv.ImgUrl = "None";
            var proptemp = await _ipropiedadService.Add(sv);

            if (proptemp.Id != 0 && proptemp != null)

                proptemp.ImgUrl = AdmFiles.UploadFile(sv.File, proptemp.Id.ToString(), "Propiedades");
                await _ipropiedadService.Update(proptemp, proptemp.Id);
             {
                SaveFotoViewModel SavingPhotos = new SaveFotoViewModel();

                SavingPhotos.ImageUrl = proptemp.ImgUrl;
                SavingPhotos.PropiedadId = proptemp.Id;
                SavingPhotos.UserId = _userLogged.Id;

                await _ifotosService.Add(SavingPhotos);

                if (sv.File2 != null)
                {
                    SavingPhotos.ImageUrl = AdmFiles.UploadFile(sv.File2, proptemp.Id.ToString(), "Propiedades");
                    await _ifotosService.Add(SavingPhotos);
                }
                if (sv.File3 != null)
                {
                    SavingPhotos.ImageUrl = AdmFiles.UploadFile(sv.File3, proptemp.Id.ToString(), "Propiedades");
                    await _ifotosService.Add(SavingPhotos);
                }
                if (sv.File4 != null)
                {
                    SavingPhotos.ImageUrl = AdmFiles.UploadFile(sv.File4, proptemp.Id.ToString(), "Propiedades");
                    await _ifotosService.Add(SavingPhotos);
                }
            }

            return RedirectToRoute(new { controller = "Agent", action = "MantenimientoPropiedades" });
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePropiedad(SavePropiedadViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                return View(sv);    
            }
            sv.UserId = _userLogged.Id;

            if (sv.File != null)
            {
                sv.ImgUrl = AdmFiles.UploadFile(sv.File, sv.Id.ToString(), "Propiedades");
            }
            await _ipropiedadService.Update(sv, sv.Id);
            return RedirectToRoute(new { controller = "Agent", action = "MantenimientoPropiedades" });

        }

        public async Task<IActionResult> UpdatePropiedad(int id)
        {
            ViewBag.TipoPropiedad = await _itipoPropiedadService.GetAllViewModel();
            ViewBag.Venta = await _itipoVentaService.GetAllViewModel();
            ViewBag.Mejoras = await _imejoraService.GetAllViewModel();

            SavePropiedadViewModel temp = await _ipropiedadService.GetByIdSaveViewModel(id);

            return View("CreatePropiedad", temp);
        }

        public async Task<IActionResult> DeletePropiedad(int id)
        {
            var usertemp = await _ipropiedadService.GetByIdSaveViewModel(id);

            return View(usertemp);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePropiedad(SavePropiedadViewModel us)
        {
            await _ipropiedadService.Delete(us.Id);

            return RedirectToRoute(new { controller = "Agent", action = "MantenimientoPropiedades" });
        }

        public async Task<IActionResult> MiPerfil()
        {

            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

       
    }
}

