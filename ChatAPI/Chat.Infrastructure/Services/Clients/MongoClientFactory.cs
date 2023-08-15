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

namespace Chat.Infrastructure.Services.Clients
{
    public class MongoClientFactory : IMongoClientFactory
    {
        private readonly ConcurrentDictionary<string, IMongoClient> _cache = new ConcurrentDictionary<string, IMongoClient>();
        private readonly MongoConnectionOptions _options;
        private readonly ILogger _logger;

        public MongoClientFactory(IOptions<MongoConnectionOptions> options, ILogger<MongoClientFactory> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public IMongoClient GetOrCreate()
        {
            using var _ = _logger.BeginScope("{Method}", $"{nameof(MongoClientFactory)}.{nameof(GetOrCreate)}");
            var name = _options.Name.Trim().ToLowerInvariant();
            using var nameScope = _logger.BeginScope("{ClientName}", name);
            if (_cache.ContainsKey(name))
            {
                _logger.LogDebug("Client retrieved from cache");
                return _cache[name];
            }

            _cache[name] = new MongoClient(new MongoUrl(_options.Url));

            using var urlScope = _logger.BeginScope("{Url}", _options.Url);
            _logger.LogInformation("Client add to cache");

            return _cache[name];
        }
    }
}
