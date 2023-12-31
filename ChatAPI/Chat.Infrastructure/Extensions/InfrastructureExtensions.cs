﻿using Chat.Application.Services.Interfaces;
using Chat.Domain.Context;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.Factories;
using Chat.Infrastructure.Repositories;
using Chat.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddMongoClient();
            services.AddMongoRepositoryFactory();
            services.AddRepositories();
            services.AddServices();
        }


        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbSetting>(options => configuration.GetSection("DbSet").Bind(options));
            services.AddSingleton<DbSetting>(sp => sp.GetRequiredService<IOptions<DbSetting>>().Value);
        }

        private static IServiceCollection AddMongoClient(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(provider =>
            {
                var dbSetting = provider.GetService<DbSetting>();
                var client = new MongoClient(dbSetting.ConnectionString);
                return client;
            });

            return services;
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMessageRepository>(provider =>
           {
               var mongoCollectionFactory = provider.GetRequiredService<IMongoCollectionFactory>();
               return new MessageRepository(mongoCollectionFactory);
           });
            services.AddScoped<IChatRepository>(provider =>
            {
                var mongoCollectionFactory = provider.GetRequiredService<IMongoCollectionFactory>();
                return new ChatRepository(mongoCollectionFactory);
            });
            services.AddScoped<IContributorRepository>(provider =>
            {
                var mongoCollectionFactory = provider.GetRequiredService<IMongoCollectionFactory>();
                return new ContributorRepository(mongoCollectionFactory);
            });
        }

        private static void AddMongoRepositoryFactory(this IServiceCollection services)
        {
            services.AddScoped<IMongoCollectionFactory, MongoCollectionFactory>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IContributorService, ContributorService>();
        }
    }
}
