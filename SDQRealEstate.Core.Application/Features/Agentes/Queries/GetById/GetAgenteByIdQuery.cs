using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Dtos.Agentes;
using SDQRealEstate.Core.Application.Enums;
using SDQRealEstate.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.Agentes.Queries.GetById
{
    public class GetAgenteByIdQuery : IRequest<AgenteResponse>
    {

        [SwaggerParameter(Description = "Debe colocar el id del agente")]
        [Required]
        public String Id { get; set; }
    }
    public class GetAgenteByIdQueryHandler : IRequestHandler<GetAgenteByIdQuery, AgenteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IManageUsersService _imanageUsersService;

        public GetAgenteByIdQueryHandler(IMapper mapper, IManageUsersService imanageUsersService)
        {
            _mapper = mapper;
            _imanageUsersService = imanageUsersService;
        }


        public async Task<AgenteResponse> Handle(GetAgenteByIdQuery request, CancellationToken cancellationToken)
        {
            var userList = await GetById(request.Id);
            if (userList == null) throw new Exception("Propiedad not found");
            return userList;

        }

        private async Task<AgenteResponse> GetById(String id)
        {
            var propiedadList = await _imanageUsersService.GetbyRolList(Roles.Agente.ToString());
            var propieda = propiedadList.FirstOrDefault(f => f.Id == id);
            return _mapper.Map<AgenteResponse>(propieda);
        }
    }

}