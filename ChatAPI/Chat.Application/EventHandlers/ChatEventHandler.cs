using Chat.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.EventHandlers
{
    public class ChatDeletedEventHandler
    {
        private readonly IMessageService _messageService;
        public ChatDeletedEventHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }

       public async void HandleChatDeleted(object sender, ChatDeletedEventArgs e)
        {
         await _messageService.DeleteAllChatBelongMessagesAsync(e.ChatId);
        }
    }
}
