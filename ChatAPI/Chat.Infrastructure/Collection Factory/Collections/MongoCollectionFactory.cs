using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Chat.Infrastructure.Collection_Factory.Databases;
using Chat.Infrastructure.Collection_Factory.Collections;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Collection_Factory.Collections
{
    public abstract class MongoCollectionFactory <TOption, TFactory> : IMongoCollectionFactory
        where TOption:MongoCollectionOptions, new()
        where TFactory : IMongoDatabaseFactory

    {
        private readonly ConcurrentDictionary<string,object> _cache = new();
        private readonly TOption _option;
        private readonly TFactory _factory;
        private readonly ILogger _logger;

        public MongoCollectionFactory(IOptions<TOption> option, TFactory factory, ILogger logger)
        {
            _option = option.Value;
            _factory = factory;
            _logger = logger;
        }

        public async ValueTask<IMongoCollection<T>> Get<T>(CancellationToken ct) where T : class, new()
        {
            using var _ = _logger.BeginScope("{Method}", $"MongoCollectionFactory.{nameof(Get)}");

            var name = _option.Name.Trim().ToLowerInvariant();
            using var nameScope = _logger.BeginScope("{ClientName}", name);

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("CollectionName not configured.");

            IMongoDatabase database = await _factory.Get(ct).ConfigureAwait(false);

            if (_cache.ContainsKey(name))
            {
                LoggerExtensions.LogDebug(_logger, "Collection retrieved from cache");
            }
            else
            {
                _cache[name] = GetMongoCollection(database, name);
                LoggerExtensions.LogInformation(_logger, "Collection added to cache");
            }

            return (IMongoCollection<T>)_cache[name];

            static IMongoCollection<T> GetMongoCollection(IMongoDatabase database, string name)
            {
                var getCollectionMethod = database!.GetType()!.GetMethod(nameof(IMongoDatabase.GetCollection));
                var definition = getCollectionMethod!.GetGenericMethodDefinition();
                var getCollection = getCollectionMethod.MakeGenericMethod(new Type[] { typeof(T) });
                var collection = getCollection!.Invoke(database!, new object[] { name, new MongoCollectionSettings() });

                return (IMongoCollection<T>)collection!;
            }
        }
    }
}
