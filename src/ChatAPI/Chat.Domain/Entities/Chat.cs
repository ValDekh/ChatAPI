using Chat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Entities
{
    public class Chat : BaseEntity
    {
        public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }
    }
}
