using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
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
        public ChatRepository(IMongoCollection<ChatEntity> entityCollection) : base(entityCollection)
        {
            _entityCollection = entityCollection;
        }

        public async Task<List<ChatEntity>> GetAllAsync()
        {
            var filter = Builders<ChatEntity>.Filter.Empty;
            return await _entityCollection.Find(filter).ToListAsync();
        }

    }
}
