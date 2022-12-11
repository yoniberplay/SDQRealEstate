using AutoMapper;
using MediatR;
using SDQRealEstate.Core.Application.Dtos.TipoPropiedades;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Queries.GetListTipoPropiedad
{
    public class GetListTipoPropiedadQuery : IRequest<IList<TipoPropiedadesReponse>>
    {
    }

    public class GetListTipoPropiedadQueryHandler : IRequestHandler<GetListTipoPropiedadQuery, IList<TipoPropiedadesReponse>>
    {
        private readonly IMapper _mapper;
        private readonly ITipoPropiedadesRepository _tipoPropiedadesRepository;

        public GetListTipoPropiedadQueryHandler(IMapper mapper, ITipoPropiedadesRepository tipoPropiedadesRepository)
        {
            _mapper = mapper;
            _tipoPropiedadesRepository = tipoPropiedadesRepository;
        }


        public async Task<IList<TipoPropiedadesReponse>> Handle(GetListTipoPropiedadQuery request, CancellationToken cancellationToken)

        {
            var userList = await _tipoPropiedadesRepository.GetAllAsync();

            return _mapper.Map<List<TipoPropiedadesReponse>>(userList);
        }
    }
}
