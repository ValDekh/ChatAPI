using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.EventHandlers.ChatEventHandlers
{
    public class ChatDeletedEventArgs : EventArgs
    {
        public Guid ChatId { get; }
        public ChatDeletedEventArgs(Guid chatId)
        {
            ChatId = chatId;
        }
    }
}
