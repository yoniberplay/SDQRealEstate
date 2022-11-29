﻿using AutoMapper;
using SDQRealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDQRealEstate.Core.Application.ViewModels.User;
using SDQRealEstate.Core.Application.Dtos.Account;

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

            //#region mapeo de post
            //CreateMap<Post, PostViewModel>()
            //    .ReverseMap()
            //    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //    .ForMember(dest => dest.LastModified, opt => opt.Ignore())
            //    .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            //CreateMap<Post, SavePostViewModel>()
            //    .ForMember(dest => dest.File, opt => opt.Ignore())
            //    .ForMember(dest => dest.NuevoComentario, opt => opt.Ignore())
            //    .ForMember(dest => dest.Idpost, opt => opt.Ignore())
            //   .ReverseMap()
            //   .ForMember(dest => dest.Created, opt => opt.Ignore())
            //   .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //   .ForMember(dest => dest.LastModified, opt => opt.Ignore())
            //   .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());
            //#endregion

            //#region mapeo de comments
            //CreateMap<Comments, CommentsViewModel>()
            //    .ReverseMap()
            //    .ForMember(dest => dest.Created, opt => opt.Ignore())
            //    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //    .ForMember(dest => dest.LastModified, opt => opt.Ignore())
            //    .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            //CreateMap<Comments, SaveCommentViewModel>()
            //   .ReverseMap()
            //   .ForMember(dest => dest.User, opt => opt.Ignore())
            //   .ForMember(dest => dest.Post, opt => opt.Ignore())
            //   .ForMember(dest => dest.Created, opt => opt.Ignore())
            //   .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //   .ForMember(dest => dest.LastModified, opt => opt.Ignore())
            //   .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());
            //#endregion

            //#region mapeo de Amistad
            //CreateMap<Friendship, FriendshipViewModel>()
            //    .ReverseMap()
            //    .ForMember(dest => dest.Created, opt => opt.Ignore())
            //    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //    .ForMember(dest => dest.LastModified, opt => opt.Ignore())
            //    .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            //CreateMap<Friendship, SaveFriendViewModel>()
            //     .ForMember(dest => dest.amigo, opt => opt.Ignore())
            //   .ReverseMap()
            //   .ForMember(dest => dest.Created, opt => opt.Ignore())
            //   .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //   .ForMember(dest => dest.LastModified, opt => opt.Ignore())
            //   .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());
            //#endregion


        }
    }
}