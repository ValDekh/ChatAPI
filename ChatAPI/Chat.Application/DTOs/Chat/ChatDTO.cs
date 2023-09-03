using Chat.Application.DTOs.Message;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.DTOs.Chat
{
    public class ChatDTO
    {
        public Guid Id { get; set; }
        public List<Guid> Users { get; set; }
    }
}
