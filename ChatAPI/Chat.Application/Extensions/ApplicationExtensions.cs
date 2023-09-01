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
    }
}
