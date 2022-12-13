using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Dtos.Propiedad;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.Features.Products.Queries.GetPropiedadtByCode;
using Swashbuckle.AspNetCore.Annotations;

namespace SDQRealEstate.Core.Application.Features.Propiedades.Queries.GetPropiedadtByCode
{
    public class GetPropiedadtByCodeQuery : IRequest<PropiedadResponse>
    {
        [SwaggerParameter(Description = "El id de la propiedad que se desea seleccionar")]
        public int? CodeId { get; set; }
    }

        public class GetPropiedadtByIdQueryHandler : IRequestHandler<GetPropiedadtByCodeQuery, PropiedadResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPropiedadRepository _propiedadRepository;

            public GetPropiedadtByIdQueryHandler(IMapper mapper, IPropiedadRepository propiedadRepository)
            {
                _mapper = mapper;
                _propiedadRepository = propiedadRepository;
            }

            public async Task<PropiedadResponse> Handle(GetPropiedadtByCodeQuery request, CancellationToken cancellationToken)
            {

            var filter = _mapper.Map<GetPropiedadtByCodeParameters>(request);
            var userList = await GetByCode(filter);
            if (userList == null) throw new Exception("Propiedad not found");
            return userList;
           
            }

        private async Task<PropiedadResponse> GetByCode(GetPropiedadtByCodeParameters filter)
        {
            var propiedadList = await _propiedadRepository.GetAllViewModelIcnlude();
            var propieda = propiedadList.FirstOrDefault(f => f.Codigo == filter.CodeId);
            return _mapper.Map<PropiedadResponse>(propieda);
        }
    }



    }