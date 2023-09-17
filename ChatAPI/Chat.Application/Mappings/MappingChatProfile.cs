using AutoMapper;
using Chat.Application.DTOs.Chat;
using Chat.Application.DTOs.Message;
using Chat.Application.Services.Converters;
using Chat.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Common.Mappings
{
    public class MappingChatProfile : Profile
    {
        public MappingChatProfile()
        {
            CreateMap<ChatEntity, ChatDTOResponse>()
                .ForMember(dest => dest.Id, opt => opt
                .MapFrom(src => ObjectIdGuidConverter.ConvertObjectIdToGuid(src.Id)))
              .ForMember(dest => dest.Users, opt => opt
                  .MapFrom(src => src.Users.Select(u => ObjectIdGuidConverter.ConvertObjectIdToGuid(u)).ToList()));

            CreateMap<ChatDTORequest, ChatEntity>()
             .ForMember(dest => dest.Users, opt => opt
                 .MapFrom(src => src.Users.Select(u => ObjectIdGuidConverter.ConvertGuidToObjectId(u)).ToList()));

            CreateMap<ObjectId, string>().ConvertUsing(src => src.ToString());
            CreateMap<string, ObjectId>().ConvertUsing(src => ObjectId.Parse(src));
        }
    }
}
