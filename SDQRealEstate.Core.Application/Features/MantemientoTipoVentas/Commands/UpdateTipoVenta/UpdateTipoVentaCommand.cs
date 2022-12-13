using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Commands.UpdateTipoPropiedad;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.MantemientoTipoVentas.Commands.UpdateTipoVenta
{
    public class UpdateTipoVentaCommand : IRequest<TipoVentaUpdateResponse>
    {
        public int Id { get; set; }

        [SwaggerParameter(Description = "El nombre del Tipo de Venta")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "La descripcion del Tipo de Venta")]
        public string Description { get; set; }
    }

    public class UpdateTipoVentaCommandHandler : IRequestHandler<UpdateTipoVentaCommand, TipoVentaUpdateResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITipoVentaRepository _itipoVentaRepository;

        public UpdateTipoVentaCommandHandler(IMapper mapper, ITipoVentaRepository itipoVentaRepository)
        {
            _mapper = mapper;
            _itipoVentaRepository = itipoVentaRepository;
        }

        public async Task<TipoVentaUpdateResponse> Handle(UpdateTipoVentaCommand command, CancellationToken cancellationToken)
        {
            var temp = await _itipoVentaRepository.GetByIdAsync(command.Id);

            if (temp == null) throw new Exception("TipoPropiedad not found");

            temp = _mapper.Map<TipoVenta>(command);

            await _itipoVentaRepository.UpdateAsync(temp, temp.Id);

            var res = _mapper.Map<TipoVentaUpdateResponse>(temp);

            return res;
        }
    }
}
