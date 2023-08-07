using Chat.Domain.Common.Interfaces;
using Chat.Infrastructure.DataAccess;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chat.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> entityCollection;
        public Repository(IOptions<DbSetting> entityDatabaseSettings)
        {
            var mongoClient = new MongoClient(entityDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(entityDatabaseSettings.Value.DatabaseName);
            entityCollection = mongoDatabase.GetCollection<T>(entityDatabaseSettings.Value.CollectionName);
        }

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(ObjectId id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
