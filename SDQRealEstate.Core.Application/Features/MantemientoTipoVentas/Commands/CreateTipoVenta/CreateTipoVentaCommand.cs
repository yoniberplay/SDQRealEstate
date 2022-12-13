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

namespace SDQRealEstate.Core.Application.Features.MantemientoTipoVentas.Commands.CreateTipoVenta
{
    public class CreateTipoVentaCommand : IRequest<int>
    {
        public int Id { get; set; }
        [SwaggerParameter(Description = "El nombre del Tipo de Venta")]
        public string Name { get; set; }
        [SwaggerParameter(Description = "La descripcion del Tipo de Venta")]
        public string Description { get; set; }
    }

    public class CreateTipoVentaCommandHandler : IRequestHandler<CreateTipoVentaCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ITipoVentaRepository _itipoVentaRepository;

        public CreateTipoVentaCommandHandler(IMapper mapper, ITipoVentaRepository itipoVentaRepository)
        {
            _mapper = mapper;
            _itipoVentaRepository = itipoVentaRepository;
        }

        public async Task<int> Handle(CreateTipoVentaCommand request, CancellationToken cancellationToken)
        {
            var temp = _mapper.Map<TipoVenta>(request);
            await _itipoVentaRepository.AddAsync(temp);
            return temp.Id;
        }
    }

}

