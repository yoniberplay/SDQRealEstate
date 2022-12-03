using AutoMapper;
using Microsoft.AspNetCore.Http;
using SDQRealEstate.Core.Application.Dtos.Account;
using SDQRealEstate.Core.Application.Helpers;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Core.Application.Interfaces.Services;
using SDQRealEstate.Core.Application.ViewModels.TipoVenta;
using SDQRealEstate.Core.Application.ViewModels.User;
using SDQRealEstate.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Services
{
    public class TipoVentaService : GenericService<SaveTipoVentaViewModel, TipoVentaViewModel, TipoVenta>, ITipoVentaService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse? userViewModel;
        private readonly IMapper _mapper;
        private readonly ITipoVentaRepository _itipoVentaRepository;

        public TipoVentaService(IHttpContextAccessor httpContextAccessor, IMapper mapper, ITipoVentaRepository itipoVentaRepository) : base(itipoVentaRepository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
            _itipoVentaRepository = itipoVentaRepository;
        }

    }
       
    }
    
