using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Dtos.Mejora;
using SDQRealEstate.Core.Application.Dtos.TipoVenta;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.MantenimientoMejoras.Queries.GetListMejora
{
    public class GetListMejoraQuery : IRequest<IList<MejoraResponse>>
    {
    }

    public class GetListTipoVentaQueryHandler : IRequestHandler<GetListMejoraQuery, IList<MejoraResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IMejoraRepository _imejoraRepository;

        public GetListTipoVentaQueryHandler(IMapper mapper, IMejoraRepository imejoraRepository)
        {
            _mapper = mapper;
            _imejoraRepository = imejoraRepository;
        }


        public async Task<IList<MejoraResponse>> Handle(GetListMejoraQuery request, CancellationToken cancellationToken)
        {
            var userList = await _imejoraRepository.GetAllAsync();

            return _mapper.Map<List<MejoraResponse>>(userList);
        }
    }
}
