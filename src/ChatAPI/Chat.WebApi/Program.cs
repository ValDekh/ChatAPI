using Chat.Domain.Common;
using Chat.Domain.Common.Interfaces;
using Chat.Infrastructure.DataAccess;
using Chat.Infrastructure.DataAccess.Contexts;
using Chat.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<DbSetting>(
    builder.Configuration.GetSection("DbSet"));
builder.Services.AddSingleton<IRepository<BaseEntity>, Repository<BaseEntity, DbSetting>>();
builder.Services.AddSingleton<ChatDbSettings>();
builder.Services.AddSingleton<MessageDbSettings>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();



