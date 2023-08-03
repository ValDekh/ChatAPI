using Chat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Entities
{
    public class Message: BaseAuditableEntity
    {
        public Guid UserId { get; set; }
        public string TextMessage { get; set; }
    }
}
