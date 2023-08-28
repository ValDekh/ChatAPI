using Chat.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(ObjectId id);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(ObjectId id, TEntity entity);

        Task DeleteAsync(ObjectId id);
    }
}
