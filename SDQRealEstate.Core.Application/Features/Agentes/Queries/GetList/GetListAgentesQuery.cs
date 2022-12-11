using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SDQRealEstate.Core.Application.Dtos.Account;
using SDQRealEstate.Core.Application.Dtos.Agentes;
using SDQRealEstate.Core.Application.Enums;
using SDQRealEstate.Core.Application.Helpers;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Core.Application.Interfaces.Services;
using SDQRealEstate.Core.Application.ViewModels.Propiedad;
using SDQRealEstate.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SDQRealEstate.Core.Application.Features.Agentes.Queries.GetList
{
    public class GetListAgentesQuery : IRequest<IList<AgenteResponse>>
    {
    }

    public class GetListAgentesQueryHandler : IRequestHandler<GetListAgentesQuery, IList<AgenteResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IPropiedadRepository _propiedadRepository;
        private readonly IManageUsersService _imanageUsersService;

        public GetListAgentesQueryHandler(IMapper mapper, IPropiedadRepository propiedadRepository, IManageUsersService imanageUsersService)
        {
            _mapper = mapper;
            _propiedadRepository = propiedadRepository;
            _imanageUsersService = imanageUsersService; 
        }


        public async Task<IList<AgenteResponse>> Handle(GetListAgentesQuery request, CancellationToken cancellationToken)
        {
            var userList = await GetAgentList(); 
            if (userList == null || userList.Count == 0) throw new Exception("Products not found");
            return userList;
        }
        private async Task<List<AgenteResponse>> GetAgentList()
        {
            var AgentesList = await _imanageUsersService.GetbyRolList(Roles.Agente.ToString());
            var proplist = await _propiedadRepository.GetAllAsync();
            if (AgentesList == null) throw new Exception("Products not found");
            List<AgenteResponse> temp = new();
            foreach (var agent in AgentesList)
            {
                agent.CantPropiedades = proplist.Where(x => x.UserId == agent.Id).Count();
                temp.Add(_mapper.Map<AgenteResponse>(agent));
            }
            
            return temp;
        }


    }

}
