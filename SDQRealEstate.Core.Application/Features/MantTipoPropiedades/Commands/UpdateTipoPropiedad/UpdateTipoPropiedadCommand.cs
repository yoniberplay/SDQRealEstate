using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Commands.UpdateTipoPropiedad
{
    public class UpdateTipoPropiedadCommand : IRequest<TipoPropiedadUpdateResponse>
    {
        public int Id { get; set; }

        [SwaggerParameter(Description = "El nombre del tipo de propiedad")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "La descripcion del tipo de propiedad")]
        public string Description { get; set; }
    }

    public class UpdateTipoPropiedadCommandHandler : IRequestHandler<UpdateTipoPropiedadCommand, TipoPropiedadUpdateResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITipoPropiedadesRepository _tipoPropiedadesRepository;

        public UpdateTipoPropiedadCommandHandler(IMapper mapper, ITipoPropiedadesRepository tipoPropiedadesRepository)
        {
            _mapper = mapper;
            _tipoPropiedadesRepository = tipoPropiedadesRepository;
        }

        public async Task<TipoPropiedadUpdateResponse> Handle(UpdateTipoPropiedadCommand command, CancellationToken cancellationToken)
        {
            var TipoPropiedad = await _tipoPropiedadesRepository.GetByIdAsync(command.Id);

            if (TipoPropiedad == null) throw new Exception("TipoPropiedad not found");

            TipoPropiedad = _mapper.Map<TipoPropiedades>(command);

            await _tipoPropiedadesRepository.UpdateAsync(TipoPropiedad, TipoPropiedad.Id);

            var productResponse = _mapper.Map<TipoPropiedadUpdateResponse>(TipoPropiedad);

            return productResponse;
        }
    }
}
