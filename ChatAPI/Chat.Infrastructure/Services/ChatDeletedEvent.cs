using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Services
{
    public class ChatDeletedEvent : INotification,EventArgs
    {
        public Guid ChatId { get; set; }
    }
}
