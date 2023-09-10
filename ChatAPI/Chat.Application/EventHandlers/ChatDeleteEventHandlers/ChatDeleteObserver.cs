using AutoMapper;
using Chat.Application.Services.Converters;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.EventHandlers.ChatEventHandlers
{
    public class ChatDeleteObserver
    {
        private readonly IMessageRepository _messageRepository;

        public ChatDeleteObserver(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async void DeleteMessagesAsync(object sender, ChatDeletedEventArgs e)
        {
            await _messageRepository.DeleteAllMessagesAsync(e.ChatId);

        }
    }
}
