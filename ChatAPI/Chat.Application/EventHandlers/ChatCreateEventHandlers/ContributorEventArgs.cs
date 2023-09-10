using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.EventHandlers.ChatCreateEventHandlers
{
    public class ContributorEventArgs 
    {
        public ObjectId UserId { get; set; }
        public ObjectId ChatId { get; set; }
    }
}
