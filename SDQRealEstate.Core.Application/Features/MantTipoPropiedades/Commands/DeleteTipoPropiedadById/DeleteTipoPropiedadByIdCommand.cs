using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Commands.DeleteTipoPropiedadById
{
    public class DeleteTipoPropiedadByIdCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "El id del tipo de propiedad que se desea eliminar")]
        public int Id { get; set; }
    }

    public class DeleteTipoPropiedadByIdCommandHandler : IRequestHandler<DeleteTipoPropiedadByIdCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ITipoPropiedadesRepository _tipoPropiedadesRepository;

        public DeleteTipoPropiedadByIdCommandHandler(IMapper mapper, ITipoPropiedadesRepository tipoPropiedadesRepository)
        {
            _mapper = mapper;
            _tipoPropiedadesRepository = tipoPropiedadesRepository;
        }

        public async Task<int> Handle(DeleteTipoPropiedadByIdCommand request, CancellationToken cancellationToken)
        {
            var tipopropiedad = await _tipoPropiedadesRepository.GetByIdAsync(request.Id);
            if (tipopropiedad == null) throw new Exception("Tipo propieda no encontrada");
            await _tipoPropiedadesRepository.DeleteAsync(tipopropiedad);
            return tipopropiedad.Id;
        }
    }
}
