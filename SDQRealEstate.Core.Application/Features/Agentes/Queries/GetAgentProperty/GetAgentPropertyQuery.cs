using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Dtos.Agentes;
using SDQRealEstate.Core.Application.Dtos.Propiedad;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.Agentes.Queries.GetAgentProperty
{
    public class GetAgentPropertyQuery : IRequest<IList<PropiedadResponse>>
    {
        public String Id { get; set; }
    }
    public class GetAgentPropertyQueryHandler : IRequestHandler<GetAgentPropertyQuery, IList<PropiedadResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IPropiedadRepository _propiedadRepository;

        public GetAgentPropertyQueryHandler(IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            _mapper = mapper;
            _propiedadRepository = propiedadRepository;
        }


        public async Task<IList<PropiedadResponse>> Handle(GetAgentPropertyQuery request, CancellationToken cancellationToken)

        {
            var filter = _mapper.Map<GetAgentPropertyParameters>(request);
            var userList = await GetByAgentId(filter);
            if(userList == null) throw new Exception("Property not found");

            return userList;
        }

        private async Task<IList<PropiedadResponse>> GetByAgentId(GetAgentPropertyParameters filter)
        {
            var propiedadList = await _propiedadRepository.GetAllViewModelIcnlude();
            var propieda = propiedadList.Where(p => p.UserId == filter.Id).ToList();

            return _mapper.Map<List<PropiedadResponse>>(propieda);
        }
    }

}