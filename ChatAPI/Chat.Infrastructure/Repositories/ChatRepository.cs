using Chat.Application.Services.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Repositories
{
    public class ChatRepository : Repository<ChatEntity>, IChatRepository
    {
        private readonly IMongoCollection<ChatEntity> _entityCollection;
        public ChatRepository(IMongoCollectionFactory collectionFactory) : base(collectionFactory.GetExistOrNewCollection<ChatEntity>("chatCollection"))
        {
            _entityCollection = collectionFactory.GetExistOrNewCollection<ChatEntity>("chatCollection");
        }

        public async Task<List<ChatEntity>> GetAllAsync()
        {
            var filter = Builders<ChatEntity>.Filter.Empty;
            return await _entityCollection.Find(filter).ToListAsync();
        }

        //public async Task UpdateAsync(ObjectId id, ChatEntity entity)
        //{
        //    var filter = Builders<ChatEntity>.Filter.Eq(x => x.Id, id);
        //    var update = Builders<ChatEntity>.Update.Set(x => x.Users, entity.Users);
        //    await _entityCollection.UpdateOneAsync(filter, update);
        //}

    }
}
