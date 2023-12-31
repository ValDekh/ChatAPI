﻿using Chat.Application.Services.Converters;
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
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly IMongoCollection<Message> _entityCollection;
        public MessageRepository(IMongoCollectionFactory collectionFactory) : base(collectionFactory.GetExistOrNewCollection<Message>("messageCollection"))
        {
            _entityCollection = collectionFactory.GetExistOrNewCollection<Message>("messageCollection");
        }

        public async Task DeleteAllMessagesAsync(Guid chatId)
        {
            ObjectId chatIdEntity = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            var listWrites = new List<WriteModel<Message>>();
            var filterDefinition = Builders<Message>.Filter.Eq(p => p.ChatId, chatIdEntity);
            listWrites.Add(new DeleteManyModel<Message>(filterDefinition));
            await _entityCollection.BulkWriteAsync(listWrites);
        }

        public async Task<List<Message>> GetAllAsync(ObjectId chatId)
        {
            var filter = Builders<Message>.Filter.Eq(x => x.ChatId, chatId);
            return await _entityCollection.Find(filter).ToListAsync();
        }

        public async Task<List<Message>> GetMessagesWithPaginationAsync(ObjectId chatId, int skip, int pageSize)
        {
            var filter = Builders<Message>.Filter.Eq(x => x.ChatId, chatId);
            var messages = await _entityCollection.Find(filter)
                .Skip(skip)
                .Limit(pageSize)
                .ToListAsync();
            return messages;
        }
    }
}
