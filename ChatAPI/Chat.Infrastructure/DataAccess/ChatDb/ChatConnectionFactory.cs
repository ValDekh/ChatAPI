using Chat.Infrastructure.DataAccess.ChatDb.Options;
using Chat.Infrastructure.DataAccess.Interfaces;
using Chat.Infrastructure.Services.Collections;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Chat.Infrastructure.DataAccess.ChatDb
{
    public sealed class ChatsCollectionFactory :
        MongoCollectionFactory<ChatCollectionOptions, IChatDbDatabaseFactory>,
        IChatCollectionFactory
    {
        public ChatsCollectionFactory(
            IOptions<ChatCollectionOptions> options,
            IChatDbDatabaseFactory databaseFactory,
            ILogger<ChatsCollectionFactory> logger)
            : base(options, databaseFactory, logger)
        {
        }
    }
}
