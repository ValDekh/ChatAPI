using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Entities
{
    public class ChatEntity : BaseEntity
    {
        public List<ObjectId> Users { get; set; }
        public List<Message> Messages { get; set; }
    }
}

