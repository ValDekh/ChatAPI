using AutoMapper;
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
    public class MappingMessageProfile : Profile
    {
        public MappingMessageProfile()
        {
            CreateMap<Message, MessageDTOResponse>()
                .ForMember(dest => dest.Id, opt => opt
                .MapFrom(src => ObjectIdGuidConverter.ConvertObjectIdToGuid(src.Id)))
             .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src =>
                    ObjectIdGuidConverter.ConvertObjectIdToGuid(src.SenderId)))
             .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src =>
                     ObjectIdGuidConverter.ConvertObjectIdToGuid(src.ReceiverId)))
             .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src =>
                     ObjectIdGuidConverter.ConvertObjectIdToGuid(src.ChatId)));

            CreateMap<MessageDTORequest, Message>()
             .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src =>
                    ObjectIdGuidConverter.ConvertGuidToObjectId(src.SenderId)))
             .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src =>
                    ObjectIdGuidConverter.ConvertGuidToObjectId(src.ReceiverId)));
        }
    }
}
