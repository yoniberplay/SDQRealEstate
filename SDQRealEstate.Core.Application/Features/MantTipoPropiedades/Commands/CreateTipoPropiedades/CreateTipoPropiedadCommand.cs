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

namespace SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Commands.CreateTipoPropiedad
{
    public class CreateTipoPropiedadCommand : IRequest<int>
    {
        public int Id { get; set; }
        [SwaggerParameter(Description = "El nombre del tipo de propiedad")]
        public string Name { get; set; }
        [SwaggerParameter(Description = "La descripcion del tipo de propiedad")]
        public string Description { get; set; }
    }

    public class CreateTipoPropiedadCommandHandler : IRequestHandler<CreateTipoPropiedadCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ITipoPropiedadesRepository _tipoPropiedadesRepository;

        public CreateTipoPropiedadCommandHandler(IMapper mapper, ITipoPropiedadesRepository tipoPropiedadesRepository)
        {
            _mapper=mapper;
            _tipoPropiedadesRepository=tipoPropiedadesRepository;
        }

        public async Task<int> Handle(CreateTipoPropiedadCommand request, CancellationToken cancellationToken)
        {
            var tipopropiedad = _mapper.Map<TipoPropiedades>(request);
            await _tipoPropiedadesRepository.AddAsync(tipopropiedad);
            return tipopropiedad.Id;
        }
    }


}
