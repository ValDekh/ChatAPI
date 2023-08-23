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
    public class MongoRepositoryFactory : IMongoRepositoryFactory
    {
        private DbSetting _dbSetting { get; }
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        public MongoRepositoryFactory(DbSetting dbSetting, IMongoClient mongoClient)
        {
            _dbSetting = dbSetting;
            _mongoClient = mongoClient;
            _database = _mongoClient.GetDatabase(_dbSetting.DatabaseName);
        }

        public IRepository<T> CreateRepository<T>(string collectionName) where T : BaseEntity
        {
            var collection = _database.GetCollection<T>(collectionName);
            return new Repository<T>(collection);
        }
    }
}
