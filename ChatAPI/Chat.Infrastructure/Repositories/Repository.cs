﻿using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;


namespace Chat.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly IMongoCollection<TEntity> _entityCollection;
        public Repository(IMongoCollection<TEntity> entityCollection)
        {
            _entityCollection = entityCollection;
        }

        public async Task<TEntity?> GetByIdAsync(ObjectId id)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
            return await _entityCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entityCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(ObjectId id, TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
            await _entityCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _entityCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
