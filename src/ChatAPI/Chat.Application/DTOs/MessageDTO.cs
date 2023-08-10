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
        //[BsonElement("_id")]
       // [BsonRepresentation(BsonType.ObjectId)]
      //  public string Id { get; set; }
      //  [BsonRepresentation(BsonType.ObjectId)]
        public string FromWho { get; set; }
      //  [BsonRepresentation(BsonType.ObjectId)]
        public string ForWho { get; set; }
        //[BsonRepresentation(BsonType.ObjectId)]
        public string ChatId { get; set; }

        public string TextMessage { get; set; }
    }
}
