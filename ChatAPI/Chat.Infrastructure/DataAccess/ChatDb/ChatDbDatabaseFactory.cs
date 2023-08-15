using Chat.Infrastructure.Collection_Factory.Clients;
using Chat.Infrastructure.DataAccess.ChatDb.Options;
using Chat.Infrastructure.DataAccess.Interfaces;
using Chat.Infrastructure.Services.Databases;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Chat.Infrastructure.DataAccess.ChatDb
{

    public sealed class ChatDbDatabaseFactory :
        MongoDatabaseFactory<ChatDbDatabaseOptions>,
        IChatDbDatabaseFactory
    {
        public ChatDbDatabaseFactory(
            IOptions<ChatDbDatabaseOptions> options,
            IMongoClientFactory factory,
            ILogger<ChatDbDatabaseFactory> logger)
            : base(options, factory, logger)
        {
        }
    }
}
