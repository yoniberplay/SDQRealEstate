using AutoMapper;
using SDQRealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDQRealEstate.Core.Application.ViewModels.User;
using SDQRealEstate.Core.Application.Dtos.Account;
using SDQRealEstate.Core.Application.ViewModels.Propiedad;
using SDQRealEstate.Core.Application.ViewModels.Fotos;
using SDQRealEstate.Core.Application.ViewModels.Mejoras;
using SDQRealEstate.Core.Application.ViewModels.TipoPropiedad;
using SDQRealEstate.Core.Application.ViewModels.TipoVenta;
using SDQRealEstate.Core.Application.Dtos.Propiedad;
using SDQRealEstate.Core.Application.Dtos.Fotos;
using SDQRealEstate.Core.Application.Dtos.TipoPropiedades;
using SDQRealEstate.Core.Application.Dtos.TipoVenta;
using SDQRealEstate.Core.Application.Dtos.Mejora;
using SDQRealEstate.Core.Application.Features.Propiedades.Queries.GetPropiedadtByCode;
using SDQRealEstate.Core.Application.Dtos.Agentes;
using SDQRealEstate.Core.Application.Features.Agentes.Queries.GetAgentProperty;
using SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Commands.CreateTipoPropiedad;
using SDQRealEstate.Core.Application.Features.MantTipoPropiedades.Commands.UpdateTipoPropiedad;
using SDQRealEstate.Core.Application.Features.MantemientoTipoVentas.Commands.CreateTipoVenta;
using SDQRealEstate.Core.Application.Features.MantemientoTipoVentas.Commands.UpdateTipoVenta;
using SDQRealEstate.Core.Application.Features.MantenimientoMejoras.Commands.CreateMejora;
using SDQRealEstate.Core.Application.Features.MantenimientoMejoras.Commands.UpdateMejora;

namespace SDQRealEstate.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region UserProfile
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region mapeo de Propiedad
            CreateMap<Propiedad, PropiedadViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Propiedad, SavePropiedadViewModel>()
                //.ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ForMember(dest => dest.File2, opt => opt.Ignore())
                .ForMember(dest => dest.File3, opt => opt.Ignore())
                .ForMember(dest => dest.File4, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(dest => dest.fotos, opt => opt.Ignore())
               .ForMember(dest => dest.tipoVenta, opt => opt.Ignore())
                .ForMember(dest => dest.tipoPropiedades, opt => opt.Ignore())
                .ForMember(dest => dest.Mejoras, opt => opt.Ignore())
               .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Propiedad, PropiedadResponse>()
                .ReverseMap();
            #endregion

            #region mapeo de Fotos
            CreateMap<Fotos, FotoViewModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.propiedad, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Fotos, SaveFotoViewModel>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.File, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(dest => dest.propiedad, opt => opt.Ignore())
               .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Fotos, FotosResponse>()
                .ReverseMap();
            #endregion

            #region mapeo de Mejora
            CreateMap<Mejora, MejoraViewModel>()
                .ForMember(dest => dest.Cantidad, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Mejora, SaveMejoraViewModel>()
               .ReverseMap()
               .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Mejora, MejoraResponse>()
               .ReverseMap();
            #endregion

            #region mapeo de TipoPropiedades

            CreateMap<TipoPropiedades, SaveTipoPropiedadViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.propiedad, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());


            CreateMap<TipoPropiedades, TipoPropiedadViewModel>()
               .ForMember(dest => dest.Cantidad, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(dest => dest.propiedad, opt => opt.Ignore())
               .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<TipoPropiedades, TipoPropiedadesReponse>()
               .ReverseMap()
               .ForMember(dest => dest.propiedad, opt => opt.Ignore());

            #endregion

            #region mapeo de TipoVenta
            CreateMap<TipoVenta, TipoVentaViewModel>()
                .ForMember(dest => dest.Cantidad, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<TipoVenta, SaveTipoVentaViewModel>()
               .ReverseMap()
               .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<TipoVenta, TipoVentaResponse>()
               .ReverseMap();
            #endregion

            #region CQRS

            CreateMap<GetPropiedadtByCodeQuery, GetPropiedadtByCodeQuery>()
              .ReverseMap();

            CreateMap<GetAgentPropertyParameters, GetAgentPropertyQuery>()
             .ReverseMap();

            CreateMap<AgenteResponse, UserViewModel>()
             .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
             .ForMember(dest => dest.Username, opt => opt.Ignore())
             .ForMember(dest => dest.Password, opt => opt.Ignore())
             .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
             .ForMember(dest => dest.Tipo, opt => opt.Ignore())
             .ForMember(dest => dest.Foto, opt => opt.Ignore())
             .ReverseMap();

            #region TipoPropiedades

            CreateMap<TipoPropiedades, CreateTipoPropiedadCommand>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.propiedad, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<UpdateTipoPropiedadCommand, TipoPropiedadUpdateResponse>()
            .ReverseMap();

            CreateMap<UpdateTipoPropiedadCommand, TipoPropiedades>()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.propiedad, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
           .ReverseMap();

            CreateMap<TipoPropiedades, TipoPropiedadUpdateResponse>()
            .ReverseMap()
            .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.propiedad, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            #endregion


            #region TipoVenta
            CreateMap<TipoVenta, CreateTipoVentaCommand>()
           .ReverseMap()
           .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.propiedad, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<TipoVenta, TipoVentaUpdateResponse>()
           .ReverseMap()
           .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.propiedad, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<TipoVentaUpdateResponse, CreateTipoVentaCommand>()
            .ReverseMap();

            CreateMap<UpdateTipoVentaCommand, TipoVenta>()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.propiedad, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
           .ReverseMap();

            CreateMap<TipoVentaResponse, TipoVenta>()
               .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.propiedad, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
          .ReverseMap();

            #endregion

            #region MEJORA
            CreateMap<Mejora, CreateMejoraCommand>()
           .ReverseMap()
           .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.propiedad, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Mejora, MejoraUpdateResponse>()
           .ReverseMap()
           .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.propiedad, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<MejoraUpdateResponse, CreateMejoraCommand>()
            .ReverseMap();

            CreateMap<UpdateMejoraaCommand, Mejora>()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.propiedad, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
           .ReverseMap();

            CreateMap<MejoraResponse, Mejora>()
               .ForMember(dest => dest.Created, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.LastModified, opt => opt.Ignore())
               .ForMember(dest => dest.propiedad, opt => opt.Ignore())
               .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
          .ReverseMap();

            #endregion




            #endregion


        }
    }
}
