using Chat.Domain.Context;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.Repositories;
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


        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbSetting>(options => configuration.GetSection("DbSet").Bind(options));
            services.AddSingleton<DbSetting>(sp => sp.GetRequiredService<IOptions<DbSetting>>().Value);
        }
    

        private static void AddRepositories<T>(this IServiceCollection services) where T:BaseEntity
        {
            services
                .AddScoped(typeof(IRepository<T>), typeof(Repository<T>));
        }

        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IMediator, Mediator>()
                .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
                .AddTransient<IDateTimeService, DateTimeService>()
                .AddTransient<IEmailService, EmailService>();
        }



    }
}
