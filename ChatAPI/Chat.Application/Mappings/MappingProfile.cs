using AutoMapper;
using Chat.Application.DTOs.Chat;
using Chat.Application.DTOs.Message;
using Chat.Domain.Common;
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
            CreateMap<ChatEntity, ChatDTO>()
             .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.ConvertAll(u => u.ToString())))
             .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

            CreateMap<Message, MessageDTO>()
             .ForMember(dest => dest.FromWho, opt => opt.MapFrom(src => src.FromWho.ToString()))
             .ForMember(dest => dest.ForWho, opt => opt.MapFrom(src => src.ForWho.ToString()))
             .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId.ToString()));

            CreateMap<ChatDTO, ChatEntity>()
             .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.ConvertAll(u => new ObjectId(u))))
             .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

            CreateMap<MessageDTO, Message>()
             .ForMember(dest => dest.FromWho, opt => opt.MapFrom(src => new ObjectId(src.FromWho)))
             .ForMember(dest => dest.ForWho, opt => opt.MapFrom(src => new ObjectId(src.ForWho)))
             .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => new ObjectId(src.ChatId)));

            CreateMap<ObjectId, string>().ConvertUsing(src => src.ToString());
            CreateMap<string, ObjectId>().ConvertUsing(src => ObjectId.Parse(src));


            //    CreateMap<ChatEntity, ChatDTO>().IncludeMembers(c => c.Id).ReverseMap();
            //    CreateMap<BaseEntity, ChatDTO>(MemberList.None);
            //    CreateMap<Message,MessageDTO>().IncludeMembers(m => m.Id).ReverseMap();
            //    CreateMap<BaseEntity, MessageDTO>(MemberList.None);
            //    CreateMap<string, ChatDTO>();
            //    CreateMap<string, MessageDTO>();
            //    CreateMap<ObjectId, ChatDTO>();
            //    CreateMap<ObjectId, MessageDTO>();
            //    CreateMap<ObjectId, string>().ConvertUsing(src => src.ToString());
            //    CreateMap<string, ObjectId>().ConvertUsing(src => ObjectId.Parse(src));
            //    CreateMap<MessageDTO, Message>().ForMember(dest => dest.Id,
            //        opt => opt.MapFrom(src => src.)) .Map(m => m.Id, m => m.Id);
            //    CreateMap<User, UserViewModel>()
            //.ForMember(dest =>
            //    dest.FName,
            //    opt => opt.MapFrom(src => src.FirstName))
        }
    }
}
