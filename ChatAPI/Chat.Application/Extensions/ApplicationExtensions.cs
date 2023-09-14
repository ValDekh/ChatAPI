using Chat.Application.Factories;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper();
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            services.AddSingleton<ObserverAndHandlerFactory>();
            services.AddSingleton(provider =>
                provider.GetRequiredService<ObserverAndHandlerFactory>().CreateChatObserver());
            services.AddSingleton(provider =>
                provider.GetRequiredService<ObserverAndHandlerFactory>().CreateChatEventHandler());
            services.AddSingleton(provider =>
                provider.GetRequiredService<ObserverAndHandlerFactory>().CreateContributorObserver());
            services.AddSingleton(provider =>
                provider.GetRequiredService<ObserverAndHandlerFactory>().CreateContributorEventHandler());
            return services;
        }
    }
}
