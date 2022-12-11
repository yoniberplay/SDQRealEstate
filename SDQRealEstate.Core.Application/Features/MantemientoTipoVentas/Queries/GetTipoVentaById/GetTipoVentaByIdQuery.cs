using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Dtos.TipoVenta;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.MantemientoTipoVentas.Queries.GetTipoVentaById
{
    public class GetTipoVentaByIdQuery : IRequest<TipoVentaResponse>
    {
        public int Id { get; set; }
    }

    public class GetTipoVentaByIdQueryHandler : IRequestHandler<GetTipoVentaByIdQuery, TipoVentaResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITipoVentaRepository _itipoVentaRepository;

        public GetTipoVentaByIdQueryHandler(IMapper mapper, ITipoVentaRepository itipoVentaRepository)
        {
            _mapper = mapper;
            _itipoVentaRepository = itipoVentaRepository;
        }

        public async Task<TipoVentaResponse> Handle(GetTipoVentaByIdQuery request, CancellationToken cancellationToken)
        {
            var userList = await _itipoVentaRepository.GetAllAsync();
            var tipo = userList.Where(t => t.Id == request.Id).FirstOrDefault();
            if (tipo == null) throw new Exception("TipoVenta not found");

            return _mapper.Map<TipoVentaResponse>(tipo);
        }

    }
}
