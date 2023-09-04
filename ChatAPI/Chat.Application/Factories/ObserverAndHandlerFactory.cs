﻿using AutoMapper;
using Chat.Application.EventHandlers.ChatEventHandlers;
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

        public ChatDeleteObserver CreateObserver()
        {
            using var scope = _serviceProvider.CreateScope();
            var messageService = scope.ServiceProvider.GetRequiredService<IMessageService>();
            return new ChatDeleteObserver(messageService);
        }

        public ChatDeletedEventHandler CreateEventHandler()
        {
            using var scope = _serviceProvider.CreateScope();
            var observer = CreateObserver();
            return new ChatDeletedEventHandler(observer);
        }
    }
}
