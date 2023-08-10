using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Common
{
    public class BaseEntity
    {
        [BsonId]
       // [BsonElement("_id")]
       // [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public ObjectId? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public ObjectId? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
