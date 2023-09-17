using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.EventHandlers.ContributorCreateEventHandlers
{
    public class ContributorCreateEventArgs : EventArgs 
    {
        public ObjectId UserId { get; set; }
        public ObjectId ChatId { get; set; }
    }
}
