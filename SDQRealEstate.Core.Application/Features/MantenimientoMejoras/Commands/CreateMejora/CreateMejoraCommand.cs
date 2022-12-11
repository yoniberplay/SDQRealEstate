using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.MantenimientoMejoras.Commands.CreateMejora
{
    public class CreateMejoraCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateMejoraCommandHandler : IRequestHandler<CreateMejoraCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IMejoraRepository _imejoraRepository;

        public CreateMejoraCommandHandler(IMapper mapper, IMejoraRepository imejoraRepository)
        {
            _mapper = mapper;
            _imejoraRepository = imejoraRepository;
        }

        public async Task<int> Handle(CreateMejoraCommand request, CancellationToken cancellationToken)
        {
            var temp = _mapper.Map<Mejora>(request);
            await _imejoraRepository.AddAsync(temp);
            return temp.Id;
        }
    }

}

