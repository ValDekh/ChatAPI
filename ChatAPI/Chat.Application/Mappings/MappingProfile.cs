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
            //CreateMap<Message, MessageDTO>().ReverseMap();
            //CreateMap<ObjectId, Guid>().ConstructUsing(objId => ObjectIdGuidConverter.ConvertObjectIdToGuid(objId));
            //CreateMap<Guid, ObjectId>().ConstructUsing(guid => ObjectIdGuidConverter.ConvertGuidToObjectId(guid));

            //CreateMap<ChatEntity, ChatDTO>()
            //.ForMember(dest => dest.Users, opt => opt
            //    .MapFrom(src => src.Users.Select(u => ObjectIdGuidConverter.ConvertObjectIdToGuid(u)).ToList()))
            //.ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

            //CreateMap<ChatDTO, ChatEntity>()
            //.ForMember(dest => dest.Users, opt => opt
            //    .MapFrom(src => src.Users.Select(u => ObjectIdGuidConverter.ConvertGuidToObjectId(u)).ToList()))
            //.ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

            CreateMap<ChatEntity, ChatDTO>()
                .ForMember(dest => dest.Id, opt => opt
                .MapFrom(src => ObjectIdGuidConverter.ConvertObjectIdToGuid(src.Id)))
              .ForMember(dest => dest.Users, opt => opt
                  .MapFrom(src => src.Users.Select(u => ObjectIdGuidConverter.ConvertObjectIdToGuid(u)).ToList()))
              .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

            CreateMap<Message, MessageDTO>()
             .ForMember(dest => dest.FromWho, opt => opt.MapFrom(src =>
                    ObjectIdGuidConverter.ConvertObjectIdToGuid(src.FromWho)))
             .ForMember(dest => dest.ForWho, opt => opt.MapFrom(src =>
                     ObjectIdGuidConverter.ConvertObjectIdToGuid(src.ForWho)))
             .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src =>
                     ObjectIdGuidConverter.ConvertObjectIdToGuid(src.ChatId)));

            CreateMap<ChatDTO, ChatEntity>()
                .ForMember(dest => dest.Id, opt => opt
                .MapFrom(src => ObjectIdGuidConverter.ConvertGuidToObjectId(src.Id)))
             .ForMember(dest => dest.Users, opt => opt
                 .MapFrom(src => src.Users.Select(u => ObjectIdGuidConverter.ConvertGuidToObjectId(u)).ToList()))
             .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

            CreateMap<MessageDTO, Message>()
             .ForMember(dest => dest.FromWho, opt => opt.MapFrom(src =>
                    ObjectIdGuidConverter.ConvertGuidToObjectId(src.FromWho)))
             .ForMember(dest => dest.ForWho, opt => opt.MapFrom(src =>
                    ObjectIdGuidConverter.ConvertGuidToObjectId(src.ForWho)))
             .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src =>
                    ObjectIdGuidConverter.ConvertGuidToObjectId(src.ChatId)));

            CreateMap<ObjectId, string>().ConvertUsing(src => src.ToString());
            CreateMap<string, ObjectId>().ConvertUsing(src => ObjectId.Parse(src));


        }
    }
}
