using Chat.Application.Extensions;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Chat.Domain.Context;
using Chat.Infrastructure.Extensions;
using Chat.WebApi.Middlewares;
using Chat.Application.Services.Interfaces;
using Chat.Application.EventHandlers.ChatEventHandler;

namespace Chat.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApplicationLayer();
            services.AddInfrastructureLayer(Configuration);
            services.AddSwaggerGen();
            services.AddTransient<ExceptionHandlingMiddleware>();
            services.AddSingleton<ChatDeletedEventHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat API"));
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            var chatDeletedEventHandler = app.ApplicationServices.GetRequiredService<ChatDeletedEventHandler>();
            var chatService = app.ApplicationServices.GetRequiredService<IChatService>();
            chatService.ChatDeleted += chatDeletedEventHandler.HandleChatDeleted;

        }

    }
}
