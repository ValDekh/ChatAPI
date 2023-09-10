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
        private readonly ChatDeleteObserver _chatDeleteObserver;
        public ChatDeletedEventHandler(ChatDeleteObserver chatObserver)
        {
           _chatDeleteObserver = chatObserver;
            OnCreate += _chatDeleteObserver.DeleteMessagesAsync;
        }
    }
}
