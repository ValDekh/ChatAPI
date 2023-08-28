using Chat.Domain.Entities;
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

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _entityCollection.Find(_ => true).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ObjectId id)
        {
            return await _entityCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entityCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(ObjectId id, TEntity entity)
        {
            await _entityCollection.ReplaceOneAsync(x => x.Id == id, entity);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _entityCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
