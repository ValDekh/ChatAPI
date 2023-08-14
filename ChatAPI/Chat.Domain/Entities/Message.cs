using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Entities
{
    public class Message: BaseEntity
    {
        public ObjectId FromWho { get; set; }
        public ObjectId ForWho { get; set; }
        public ObjectId ChatId { get; set; }

        public string TextMessage { get; set; }
    }
}
