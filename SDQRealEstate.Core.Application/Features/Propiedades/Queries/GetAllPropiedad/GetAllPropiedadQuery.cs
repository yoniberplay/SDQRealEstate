using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SDQRealEstate.Core.Application.Dtos.Account;
using SDQRealEstate.Core.Application.Dtos.Propiedad;
using SDQRealEstate.Core.Application.Helpers;
using SDQRealEstate.Core.Application.Interfaces.Repositories;
using SDQRealEstate.Core.Application.ViewModels.Propiedad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SDQRealEstate.Core.Application.Features.Propiedades.Queries.GetAllPropiedad
{
    public class GetAllPropiedadQuery : IRequest<IList<PropiedadResponse>>
    {
}
    public class GetAllPropiedadQueryHandler : IRequestHandler<GetAllPropiedadQuery, IList<PropiedadResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IPropiedadRepository _propiedadRepository;

        public GetAllPropiedadQueryHandler( IMapper mapper, IPropiedadRepository propiedadRepository) 
        {          
            _mapper = mapper;
            _propiedadRepository = propiedadRepository;
        }


        public async Task<IList<PropiedadResponse>> Handle(GetAllPropiedadQuery request, CancellationToken cancellationToken)

        {
            var userList = await _propiedadRepository.GetAllViewModelIcnlude();

            return _mapper.Map<List<PropiedadResponse>>(userList);
        }
    }
    


}
