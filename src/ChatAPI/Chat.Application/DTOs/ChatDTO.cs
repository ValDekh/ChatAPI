using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.DTOs
{
    public class ChatDTO
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public ObjectId Id { get; set; }
        //[BsonRepresentation(BsonType.ObjectId)]
        public List<string> Users { get; set; }
        public List<MessageDTO> Messages { get; set; }
    }
}
