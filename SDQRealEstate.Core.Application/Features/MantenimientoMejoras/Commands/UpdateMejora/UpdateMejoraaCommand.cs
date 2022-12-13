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

namespace SDQRealEstate.Core.Application.Features.MantenimientoMejoras.Commands.UpdateMejora
{
    public class UpdateMejoraaCommand : IRequest<MejoraUpdateResponse>
    {
        public int Id { get; set; }

        [SwaggerParameter(Description = "El nombre de la mejora")]
        public string Name { get; set; }

        [SwaggerParameter(Description = "La descripcion de la mejora")]
        public string Description { get; set; }
    }

    public class UpdateMejoraaCommandHandler : IRequestHandler<UpdateMejoraaCommand, MejoraUpdateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMejoraRepository _imejoraRepository;

        public UpdateMejoraaCommandHandler(IMapper mapper, IMejoraRepository imejoraRepository)
        {
            _mapper = mapper;
            _imejoraRepository = imejoraRepository;
        }


        public async Task<MejoraUpdateResponse> Handle(UpdateMejoraaCommand command, CancellationToken cancellationToken)
        {
            var temp = await _imejoraRepository.GetByIdAsync(command.Id);

            if (temp == null) throw new Exception("Mejora not found");

            temp = _mapper.Map<Mejora>(command);

            await _imejoraRepository.UpdateAsync(temp, temp.Id);

            var res = _mapper.Map<MejoraUpdateResponse>(temp);

            return res;
        }
    }
}
