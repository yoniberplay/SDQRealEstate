using AutoMapper;
using Microsoft.AspNetCore.Http;
using SDQRealEstate.Core.Application.Dtos.Account;
using SDQRealEstate.Core.Application.Helpers;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Core.Application.Interfaces.Services;
using SDQRealEstate.Core.Application.ViewModels.Fotos;
using SDQRealEstate.Core.Application.ViewModels.User;
using SDQRealEstate.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Services
{
    public class FotosService : GenericService<SaveFotoViewModel, FotoViewModel, Fotos>, IFotosService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse? userViewModel;
        private readonly IMapper _mapper;
        private readonly IFotosRepository _ifotosRepository;

        public FotosService(IHttpContextAccessor httpContextAccessor, IMapper mapper, IFotosRepository ifotosRepository) : base(ifotosRepository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
            _ifotosRepository = ifotosRepository;
        }

        public override async Task<SaveFotoViewModel> Add(SaveFotoViewModel vm)
        {
            vm.UserId = userViewModel.Id;

            return await base.Add(vm);
        }

        public override async Task Update(SaveFotoViewModel vm, int id)
        {
            vm.UserId = userViewModel.Id;

            await base.Update(vm,id);
        }


    }
       
    }
    
