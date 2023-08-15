using Chat.Application.Extensions;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.DataAccess;
using Chat.Infrastructure.DataAccess.Contexts;
using Chat.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Chat.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });



    }
}