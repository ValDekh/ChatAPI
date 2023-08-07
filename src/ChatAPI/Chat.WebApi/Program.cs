using Chat.Infrastructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<DbSetting>

var app = builder.Build();


