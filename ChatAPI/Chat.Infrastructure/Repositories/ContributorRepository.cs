using Chat.Application.Services.Converters;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Repositories
{
    public class ContributorRepository : Repository<Contributor>, IContributorRepository
    {
        private readonly IMongoCollection<Contributor> _contributors;   
        public ContributorRepository(IMongoCollectionFactory collectionFactory) :
            base(collectionFactory.GetExistOrNewCollection<Contributor>("contributorCollection"))
        {
            _contributors = collectionFactory.GetExistOrNewCollection<Contributor>("contributorCollection");
        }

        public async Task DeleteAllContributersAsync(Guid chatId)
        {
            ObjectId chatIdEntity = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            var listWrites = new List<WriteModel<Contributor>>();
            var filterDefinition = Builders<Contributor>.Filter.Eq(p => p.ChatId, chatIdEntity);
            listWrites.Add(new DeleteManyModel<Contributor>(filterDefinition));
            await _contributors.BulkWriteAsync(listWrites);
        }

        public async Task<List<Contributor>> GetAllAsync(ObjectId chatId)
        {
            var filter = Builders<Contributor>.Filter.Eq(x => x.ChatId, chatId);
            return await _contributors.Find(filter).ToListAsync();
        }


    }
}
