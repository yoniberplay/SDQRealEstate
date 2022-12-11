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

namespace SDQRealEstate.Core.Application.Features.Propiedades.Queries.GetPropiedadtById
{
    public class GetPropiedadtByIdQuery : IRequest<PropiedadResponse>
    {
        public int Id { get; set; }
    }

        public class GetPropiedadtByIdQueryHandler : IRequestHandler<GetPropiedadtByIdQuery, PropiedadResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPropiedadRepository _propiedadRepository;

            public GetPropiedadtByIdQueryHandler(IMapper mapper, IPropiedadRepository propiedadRepository)
            {
                _mapper = mapper;
                _propiedadRepository = propiedadRepository;
            }


            public async Task<PropiedadResponse> Handle(GetPropiedadtByIdQuery request, CancellationToken cancellationToken)
            {
                var userList = await GetById(request.Id);
            if (userList == null) throw new Exception("Propiedad not found");
            return userList;
           
            }

        private async Task<PropiedadResponse> GetById(int id)
        {
            var propiedadList = await _propiedadRepository.GetAllViewModelIcnlude();
            var propieda = propiedadList.FirstOrDefault(f => f.Id == id);
            return _mapper.Map<PropiedadResponse>(propieda);
        }
    }



    }