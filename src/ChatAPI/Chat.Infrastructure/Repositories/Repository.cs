using Chat.Domain.Common;
using Chat.Domain.Common.Interfaces;
using Chat.Infrastructure.DataAccess;
using Chat.Infrastructure.DataAccess.Interfaces;
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
    public class Repository<T, K> : IRepository<T>
        where T : BaseEntity
        where K : DbSetting
    {
        private readonly IMongoCollection<T> entityCollection;
        private readonly K _dbEntity;
        public Repository(IOptions<K> entityDatabaseSettings, K dbEntity)
        {
            _dbEntity = dbEntity;
            var mongoClient = new MongoClient(entityDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(entityDatabaseSettings.Value.DatabaseName);
            entityCollection = mongoDatabase.GetCollection<T>(_dbEntity.CollectionName);
        }
        //public Repository(K dbEntity)
        //{
        //    _dbEntity = dbEntity;
        //    var mongoClient = new MongoClient(dbEntity.ConnectionString);
        //    var mongoDatabase = mongoClient.GetDatabase(dbEntity.DatabaseName);
        //    entityCollection = mongoDatabase.GetCollection<T>(_dbEntity.CollectionName);

        //}




        public async Task<List<T>> GetAllAsync()
        {
            return await entityCollection.Find(_ => true).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(ObjectId id)
        {
            return await entityCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await entityCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(ObjectId id, T entity)
        {
            await entityCollection.ReplaceOneAsync(x => x.Id == id, entity);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await entityCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
