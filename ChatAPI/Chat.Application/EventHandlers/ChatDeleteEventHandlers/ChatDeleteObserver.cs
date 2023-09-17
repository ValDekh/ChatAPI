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
        private readonly IContributorRepository _contributorRepository;

        public ChatDeleteObserver(IMessageRepository messageRepository, IContributorRepository contributorRepository)
        {
            _messageRepository = messageRepository;
            _contributorRepository = contributorRepository;
        }

        public async void DeleteMessagesAndContribAsync(object sender, ChatDeletedEventArgs e)
        {
            await _messageRepository.DeleteAllMessagesAsync(e.ChatId);
            await _contributorRepository.DeleteAllContributersAsync(e.ChatId);

        }
    }
}
