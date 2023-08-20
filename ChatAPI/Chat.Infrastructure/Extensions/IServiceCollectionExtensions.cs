﻿using Chat.Application.Services.Interfaces;
using Chat.Domain.Context;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.Repositories;
using Chat.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepositories();
            services.AddServices();
        }


        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbSetting>(options => configuration.GetSection("DbSet").Bind(options));
            services.AddSingleton<DbSetting>(sp => sp.GetRequiredService<IOptions<DbSetting>>().Value);
        }
    

        private static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IChatServices<,>), typeof(ChatServices<,>));
        }
    }
}
