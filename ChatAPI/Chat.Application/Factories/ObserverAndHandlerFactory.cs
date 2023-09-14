using AutoMapper;
using Chat.Application.EventHandlers.ChatEventHandlers;
using Chat.Application.EventHandlers.ContributorCreateEventHandlers;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Factories
{
    public class ObserverAndHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ObserverAndHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ChatDeleteObserver CreateChatObserver()
        {
            using var scope = _serviceProvider.CreateScope();
            var messageRepository = scope.ServiceProvider.GetRequiredService<IMessageRepository>();
            var contributorRepository = scope.ServiceProvider.GetRequiredService<IContributorRepository>();
            return new ChatDeleteObserver(messageRepository, contributorRepository);
        }

        public ChatDeletedEventHandler CreateChatEventHandler()
        {
            using var scope = _serviceProvider.CreateScope();
            var observer = CreateChatObserver();
            return new ChatDeletedEventHandler(observer);
        }

        public ContributorCreateObserver CreateContributorObserver()
        {
            using var scope = _serviceProvider.CreateScope();
            var contributorRepository = scope.ServiceProvider.GetRequiredService<IContributorRepository>();
            return new ContributorCreateObserver(contributorRepository);
        }

        public ContributorCreatedEventHandler CreateContributorEventHandler()
        {
            using var scope = _serviceProvider.CreateScope();
            var observer = CreateContributorObserver();
            return new ContributorCreatedEventHandler(observer);
        }
    }
}
