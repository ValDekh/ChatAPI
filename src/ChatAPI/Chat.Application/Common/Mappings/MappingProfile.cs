using AutoMapper;
using Chat.Application.DTOs;
using Chat.Domain.Common;
using Chat.Domain.Entities;
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
            CreateMap<ChatEntity, ChatDTO>().IncludeMembers(c => c.Id).ReverseMap();
            CreateMap<BaseEntity, ChatDTO>(MemberList.None);
            CreateMap<Message,MessageDTO>().IncludeMembers(m => m.Id).ReverseMap();
            CreateMap<BaseEntity, MessageDTO>(MemberList.None);
        }
    }
}
