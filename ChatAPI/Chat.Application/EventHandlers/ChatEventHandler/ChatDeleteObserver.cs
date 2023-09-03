using AutoMapper;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.EventHandlers.ChatEventHandler
{
    public class ChatDeleteObserver
    {
        private readonly IMessageService _messageService;

        public ChatDeleteObserver(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async void DeleteMessagesAsync(object sender, ChatDeletedEventArgs e)
        {
            await _messageService.DeleteAllChatBelongMessagesAsync(e.ChatId);
        }
    }
}
