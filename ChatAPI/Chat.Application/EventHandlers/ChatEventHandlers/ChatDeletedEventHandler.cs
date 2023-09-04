using Chat.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.EventHandlers.ChatEventHandlers
{
    public class ChatDeletedEventHandler : BaseEventHandler<ChatDeletedEventArgs>
    {
        private readonly ChatObserver _chatDeleteObserver;
        public ChatDeletedEventHandler(ChatObserver chatObserver)
        {
           _chatDeleteObserver = chatObserver;
            OnCreate += _chatDeleteObserver.DeleteMessagesAsync;
        }
    }
}
