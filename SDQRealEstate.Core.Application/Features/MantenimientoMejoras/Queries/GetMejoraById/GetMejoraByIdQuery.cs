using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Dtos.Mejora;
using SDQRealEstate.Core.Application.Dtos.TipoVenta;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.MantenimientoMejoras.Queries.GetMejoraById
{
    public class GetMejoraByIdQuery : IRequest<MejoraResponse>
    {
        [SwaggerParameter(Description = "El id de la mejora que se desea seleccionar")]
        public int Id { get; set; }
    }

    public class GetMejoraByIdQueryHandler : IRequestHandler<GetMejoraByIdQuery, MejoraResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMejoraRepository _imejoraRepository;

        public GetMejoraByIdQueryHandler(IMapper mapper, IMejoraRepository imejoraRepository)
        {
            _mapper = mapper;
            _imejoraRepository = imejoraRepository;
        }
        public async Task<MejoraResponse> Handle(GetMejoraByIdQuery request, CancellationToken cancellationToken)
        {
            var userList = await _imejoraRepository.GetAllAsync();
            var tipo = userList.Where(t => t.Id == request.Id).FirstOrDefault();
            if (tipo == null) throw new Exception("Mejora not found");

            return _mapper.Map<MejoraResponse>(tipo);
        }

    }
}
