using AutoMapper;
using Chat.Application.DTOs.Contributer;
using Chat.Application.DTOs.Message;
using Chat.Application.Services.Converters;
using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Mappings
{
    public class MappingContributorProfile : Profile
    {
        public MappingContributorProfile()
        {
            CreateMap<ContributorDTORequest, Contributor>()
             .ForMember(dest => dest.UserId, opt => opt.MapFrom(src =>
                    ObjectIdGuidConverter.ConvertGuidToObjectId(src.UserId)))
             .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.GetSelectedPermissions()));

            CreateMap<Contributor, ContributorDTOResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => ObjectIdGuidConverter.ConvertObjectIdToGuid(src.Id)))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => ObjectIdGuidConverter.ConvertObjectIdToGuid(src.UserId)))
            .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => ObjectIdGuidConverter.ConvertObjectIdToGuid(src.ChatId)))
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions));

        }
    }
}
