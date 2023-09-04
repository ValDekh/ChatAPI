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
        public ChatDeletedEventHandler(ChatDeleteObserver chatDeleteObserver)
        {
           _chatDeleteObserver = chatDeleteObserver;
            OnCreate += _chatDeleteObserver.DeleteMessagesAsync;
        }
    }
}
