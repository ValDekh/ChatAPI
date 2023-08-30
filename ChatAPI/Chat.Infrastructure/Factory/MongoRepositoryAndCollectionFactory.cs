using Chat.Application.Services.Interfaces;
using Chat.Domain.Context;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Factory
{
    public class MongoRepositoryAndCollectionFactory : IMongoRepositoryAndCollectionFactory
    {
        private DbSetting _dbSetting { get; }
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        public MongoRepositoryAndCollectionFactory(DbSetting dbSetting, IMongoClient mongoClient)
        {
            _dbSetting = dbSetting;
            _mongoClient = mongoClient;
            _database = _mongoClient.GetDatabase(_dbSetting.DatabaseName);
        }

        public IRepository<T> Repository<T>(string collectionName) where T : BaseEntity
        {
            var collection = _database.GetCollection<T>(collectionName);
            return new Repository<T>(collection);
        }

        public IMongoCollection<T> GetExistCollection<T>(string collectionName) where T : BaseEntity
        {
            var collection = _database.GetCollection<T>(collectionName);
            return collection;
        }
    }
}
