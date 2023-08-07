using Chat.Domain.Common;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Entities
{
    public class Message: BaseEntity
    {
        public ObjectId UserId { get; set; }
        public string TextMessage { get; set; }
    }
}
