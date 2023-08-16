using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.DataAccess;
using Chat.Infrastructure.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Factory
{
    public class MongoRepositoryFactory
    {
        public DbSetting _dbSetting { get; }
        private readonly IMongoDatabase _database;
        public MongoRepositoryFactory(DbSetting dbSetting)
        {
            _dbSetting = dbSetting;
            var client = new MongoClient(_dbSetting.ConnectionString);
            _database = client.GetDatabase(_dbSetting.ConnectionString);
        }

        public IRepository<T> CreateRepository<T>(string collectionName) where T : BaseEntity
        {
            var collection = _database.GetCollection<T>(collectionName);
            return new Repository<T>(collection);
        }
    }
}
