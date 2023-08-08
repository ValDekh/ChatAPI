using Chat.Domain.Common;
using Chat.Domain.Common.Interfaces;
using Chat.Infrastructure.DataAccess;
using Chat.Infrastructure.DataAccess.Contexts;
using Chat.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<DbSetting>(
    builder.Configuration.GetSection(nameof(DbSetting)));
builder.Services.AddSingleton<DbSetting>(sp => sp.GetRequiredService<IOptions<DbSetting>>().Value);
//builder.Configuration.GetSection("DbSet"));
builder.Services.AddScoped(typeof(IRepository<BaseEntity>), typeof(Repository<BaseEntity, DbSetting>));
//builder.Services.AddSingleton<MessageDbSettings>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat API"));
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();