using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
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
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly IMongoCollection<T> _entityCollection;
        public Repository(IMongoCollection<T> entityCollection)
        {
            _entityCollection= entityCollection;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _entityCollection.Find(_ => true).ToListAsync();
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
