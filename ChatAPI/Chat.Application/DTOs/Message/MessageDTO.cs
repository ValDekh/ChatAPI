using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.DTOs.Message
{
    public class MessageDTO
    {
        public Guid FromWho { get; set; }
        public Guid ForWho { get; set; }
        public Guid ChatId { get; set; }

        public string TextMessage { get; set; }
    }
}
