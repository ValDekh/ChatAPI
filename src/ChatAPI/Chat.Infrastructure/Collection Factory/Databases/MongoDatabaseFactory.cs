using Chat.Infrastructure.Collection_Factory.Clients;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Collection_Factory.Databases
{
    public abstract class MongoDatabaseFactory<TOptions> : IMongoDatabaseFactory where TOptions : MongoDatabaseOptions, new()
    {
        private readonly ConcurrentDictionary<string, IMongoDatabase> _cache = new ConcurrentDictionary<string, IMongoDatabase>();
        TOptions _options;
        IMongoClientFactory _clientFactory;
        ILogger _logger;

        public MongoDatabaseFactory(IOptions<TOptions> options, IMongoClientFactory factory, ILogger logger)
        {
            _clientFactory = factory;
            _options = options.Value;
            _logger = logger;
        }


        public async ValueTask<IMongoDatabase> Get(CancellationToken ct)
        {
            using var _ = _logger.BeginScope("{Method}", $"{nameof(MongoDatabaseFactory<TOptions>)}.{nameof(Get)}");
            var name = _options.Name.Trim().ToLowerInvariant();
            using var nameScope = _logger.BeginScope("{ClientName}", name);
            if (_cache.ContainsKey(name))
            {
                LoggerExtensions.LogDebug(_logger, "Database retrieved from cache");
                return _cache[name];
            }

            var client = _clientFactory.GetOrCreate();
            var names = await (await client.ListDatabaseNamesAsync().ConfigureAwait(false)).ToListAsync(ct).ConfigureAwait(false);
            if (!names.Any(x => x.Equals(name, StringComparison.InvariantCultureIgnoreCase))) 
            {
                throw new ArgumentOutOfRangeException($"Database: {name} doesn't exist.");
            }

            _cache[name] = client.GetDatabase(name);
            LoggerExtensions.LogInformation(_logger, "Database added to cache");

            return _cache[name];

        }
    }
}
