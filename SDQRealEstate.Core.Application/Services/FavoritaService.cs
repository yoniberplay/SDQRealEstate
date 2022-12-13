using AutoMapper;
using Microsoft.AspNetCore.Http;
using SDQRealEstate.Core.Application.Dtos.Account;
using SDQRealEstate.Core.Application.Helpers;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Core.Application.Interfaces.Services;
using SDQRealEstate.Core.Application.ViewModels.Favorita;
using SDQRealEstate.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Services
{
    public class FavoritaService : GenericService<SaveFavoritaViewModel, FavoritaViewModel, Favorita>, IFavoritaService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse? userViewModel;
        private readonly IMapper _mapper;
        private readonly IFavoritaRepository _ifavoritaRepository;

        public FavoritaService(IHttpContextAccessor httpContextAccessor, IMapper mapper, IFavoritaRepository ifavoritaRepository) : base(ifavoritaRepository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
            _ifavoritaRepository = ifavoritaRepository;
        }

        public async Task<List<FavoritaViewModel>> GetAllWithProperties()
        {
              var list = await _ifavoritaRepository.GetAllWithProperties();
            var temp = _mapper.Map<List<FavoritaViewModel>>(list);

            return temp;
        }
    }
       
    }
    
