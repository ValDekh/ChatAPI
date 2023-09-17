using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Entities
{
    public class Contributor : BaseEntity
    {

        public ObjectId UserId { get; set; }
        public ObjectId ChatId { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
