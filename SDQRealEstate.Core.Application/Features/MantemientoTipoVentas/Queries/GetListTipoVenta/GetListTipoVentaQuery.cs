using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Dtos.TipoVenta;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.MantemientoTipoVentas.Queries.GetListTipoVenta
{
    public class GetListTipoVentaQuery : IRequest<IList<TipoVentaResponse>>
    {
    }

    public class GetListTipoVentaQueryHandler : IRequestHandler<GetListTipoVentaQuery, IList<TipoVentaResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ITipoVentaRepository _itipoVentaRepository;

        public GetListTipoVentaQueryHandler(IMapper mapper, ITipoVentaRepository itipoVentaRepository) { 
        
            _mapper = mapper;
            _itipoVentaRepository = itipoVentaRepository;
        }


        public async Task<IList<TipoVentaResponse>> Handle(GetListTipoVentaQuery request, CancellationToken cancellationToken)
        {
            var userList = await _itipoVentaRepository.GetAllAsync();

            return _mapper.Map<List<TipoVentaResponse>>(userList);
        }
    }
}
