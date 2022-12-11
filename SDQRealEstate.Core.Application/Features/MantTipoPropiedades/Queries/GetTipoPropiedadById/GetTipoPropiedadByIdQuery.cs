using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Dtos.TipoPropiedades;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Queries.GetTipoPropiedadById
{
    public class GetTipoPropiedadByIdQuery : IRequest<TipoPropiedadesReponse>
    {
        public int Id { get; set; }
    }

    public class GetTipoPropiedadByIdQueryHandler : IRequestHandler<GetTipoPropiedadByIdQuery, TipoPropiedadesReponse>
    {
        private readonly IMapper _mapper;
        private readonly ITipoPropiedadesRepository _tipoPropiedadesRepository;

        public GetTipoPropiedadByIdQueryHandler(IMapper mapper, ITipoPropiedadesRepository tipoPropiedadesRepository)
        {
            _mapper = mapper;
            _tipoPropiedadesRepository = tipoPropiedadesRepository;
        }


        public async Task<TipoPropiedadesReponse> Handle(GetTipoPropiedadByIdQuery request, CancellationToken cancellationToken)

        {
            var userList = await _tipoPropiedadesRepository.GetAllAsync();
            var tipo = userList.Where(t => t.Id == request.Id).FirstOrDefault();
            if (tipo == null) throw new Exception("TipoPropiedad not found");

            return _mapper.Map<TipoPropiedadesReponse>(tipo);
        }
    }
}
