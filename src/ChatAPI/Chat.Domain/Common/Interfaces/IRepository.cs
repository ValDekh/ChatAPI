using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Common.Interfaces
{
    public interface IRepository <T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(ObjectId id);
        Task AddAsync(T entity);
        Task UpdateAsync(ObjectId id , T entity);
        Task DeleteAsync(ObjectId id);
    }
}
