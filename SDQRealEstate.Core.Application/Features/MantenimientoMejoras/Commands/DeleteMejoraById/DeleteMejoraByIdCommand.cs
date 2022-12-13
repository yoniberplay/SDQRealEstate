using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.MantenimientoMejoras.Commands.DeleteMejoraById
{
    public class DeleteMejoraByIdCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "El id de la mejora que se desea eliminar")]
        public int Id { get; set; }
    }

    public class DeleteMejoraByIdCommandHandler : IRequestHandler<DeleteMejoraByIdCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IMejoraRepository _imejoraRepository;

        public DeleteMejoraByIdCommandHandler(IMapper mapper, IMejoraRepository imejoraRepository)
        {
            _mapper = mapper;
            _imejoraRepository = imejoraRepository;
        }

        public async Task<int> Handle(DeleteMejoraByIdCommand request, CancellationToken cancellationToken)
        {
            var tv = await _imejoraRepository.GetByIdAsync(request.Id);
            if (tv == null) throw new Exception("Mejora no encontrada");
            await _imejoraRepository.DeleteAsync(tv);
            return tv.Id;
        }
    }

}
