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
    public class ChatObserver
    {
        private readonly IMongoCollectionFactory _mongoCollectionFactory;
        private readonly IMessageRepository _messageRepository;

        public ChatObserver(IMongoCollectionFactory mongoCollectionFactory, IMessageRepository messageRepository)
        {
            _mongoCollectionFactory = mongoCollectionFactory;
            _messageRepository = messageRepository;
        }

        public async void DeleteMessagesAsync(object sender, ChatDeletedEventArgs e)
        {
            await _messageRepository.DeleteAllMessagesAsync(e.ChatId);

        }
    }
}
