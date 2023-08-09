using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.DTOs
{
    public class MessageDTO
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        public ObjectId FromWho { get; set; }
        public ObjectId ForWho { get; set; }
        public ObjectId ChatId { get; set; }

        public string TextMessage { get; set; }
    }
}
