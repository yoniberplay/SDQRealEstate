using AutoMapper;
using Microsoft.AspNetCore.Http;
using SDQRealEstate.Core.Application.Dtos.Account;
using SDQRealEstate.Core.Application.Helpers;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Core.Application.Interfaces.Services;
using SDQRealEstate.Core.Application.ViewModels.Propiedad;
using SDQRealEstate.Core.Application.ViewModels.User;
using SDQRealEstate.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Services
{
    public class PropiedadService : GenericService<SavePropiedadViewModel, PropiedadViewModel, Propiedad>, IPropiedadService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse? userViewModel;
        private readonly IMapper _mapper;
        private readonly IPropiedadRepository _propiedadRepository;

        public PropiedadService(IHttpContextAccessor httpContextAccessor, IMapper mapper, IPropiedadRepository propiedadRepository) : base(propiedadRepository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
            _propiedadRepository = propiedadRepository;
        }

        public async Task<int> GetCantidadTipoPropiedad(int id)
        {
            var userList = await _propiedadRepository.GetAllAsync();
            return userList.Where(x => x.TipoPropiedadId == id).ToList().Count();

        }
        public async Task<int> GetCantidadTipoVenta(int id)
        {
            var userList = await _propiedadRepository.GetAllAsync();
            return userList.Where(x => x.TipoVentaId == id).ToList().Count();

        }

        public async Task<int> GetCantidadMejora(int id)
        {
            var userList = await _propiedadRepository.GetAllAsync();
            return userList.Where(x => x.MejorasId == id).ToList().Count();
        }

        public async Task<List<PropiedadViewModel>> GetAllPropiedadByAgentId(String AgentId)
        {
            var temp = await GetAllViewModel();
            return temp.Where(x => x.UserId == AgentId).ToList();

        }

        public virtual async Task<List<PropiedadViewModel>> GetAllViewModelIcnlude()
        {
            var userList = await _propiedadRepository.GetAllViewModelIcnlude();

            return _mapper.Map<List<PropiedadViewModel>>(userList);

        }

        public async Task<PropiedadViewModel> GetByCode(int code)
        {
            var temp = await GetAllViewModel();
            
            return temp.Where(x => x.Codigo == code).ToList().FirstOrDefault();
        }
    }
       
    }
    
