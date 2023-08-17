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
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, MessageDTO>().ReverseMap();
            CreateMap<ObjectId, Guid>().ConstructUsing(objId => ObjectIdGuidConverter.ConvertObjectIdToGuid(objId));
            CreateMap<Guid, ObjectId>().ConstructUsing(guid => ObjectIdGuidConverter.ConvertGuidToObjectId(guid));

            CreateMap<ChatEntity, ChatDTO>()
            .ForMember(dest => dest.Users, opt => opt
                .MapFrom(src => src.Users.Select(u => ObjectIdGuidConverter.ConvertObjectIdToGuid(u)).ToList()))
            .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

            CreateMap<ChatDTO, ChatEntity>()
            .ForMember(dest => dest.Users, opt => opt
                .MapFrom(src => src.Users.Select(u => ObjectIdGuidConverter.ConvertGuidToObjectId(u)).ToList()))
            .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));


            //CreateMap<ChatEntity, ChatDTO>()
            // .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.ConvertAll(u => Guid.Parse(u))))
            // .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

            //CreateMap<Message, MessageDTO>()
            // .ForMember(dest => dest.FromWho, opt => opt.MapFrom(src => src.FromWho.ToString()))
            // .ForMember(dest => dest.ForWho, opt => opt.MapFrom(src => src.ForWho.ToString()))
            // .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId.ToString()));

            //CreateMap<ChatDTO, ChatEntity>()
            // .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.ConvertAll(u => u.ToString()))
            // .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

            //CreateMap<MessageDTO, Message>()
            // .ForMember(dest => dest.FromWho, opt => opt.MapFrom(src => new ObjectId(src.FromWho)))
            // .ForMember(dest => dest.ForWho, opt => opt.MapFrom(src => new ObjectId(src.ForWho)))
            // .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => new ObjectId(src.ChatId)));

            //CreateMap<ObjectId, string>().ConvertUsing(src => src.ToString());
            //CreateMap<string, ObjectId>().ConvertUsing(src => ObjectId.Parse(src));

        }
    }
}
