using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.MantemientoTipoVentas.Commands.DeleteTipoVentaById
{
    public class DeleteTipoVentaByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteTipoVentaByIdCommandHandler : IRequestHandler<DeleteTipoVentaByIdCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ITipoVentaRepository _itipoVentaRepository;

        public DeleteTipoVentaByIdCommandHandler(IMapper mapper, ITipoVentaRepository itipoVentaRepository)
        {
            _mapper = mapper;
            _itipoVentaRepository = itipoVentaRepository;
        }

        public async Task<int> Handle(DeleteTipoVentaByIdCommand request, CancellationToken cancellationToken)
        {
            var tv = await _itipoVentaRepository.GetByIdAsync(request.Id);
            if (tv == null) throw new Exception("Tipo propieda no encontrada");
            await _itipoVentaRepository.DeleteAsync(tv);
            return tv.Id;
        }
    }

}
