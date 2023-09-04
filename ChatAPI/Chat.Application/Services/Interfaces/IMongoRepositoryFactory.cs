using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.Interfaces
{
    public interface IMongoRepositoryAndCollectionFactory
      
    {
        IMongoCollection<TEntity> GetExistOrNewCollection<TEntity>(string collectionName) where TEntity : BaseEntity;
    }
}
