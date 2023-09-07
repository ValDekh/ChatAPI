using Chat.Domain.Structures.Enums;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public ObjectId UserId { get; set; }
        public ObjectId ChatId { get; set; }
        public PermissionsEnum permissions { get; set; }
    }
}
