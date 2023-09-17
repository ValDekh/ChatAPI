using Chat.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Interfaces
{
    public interface IContributorRepository : IRepository<Contributor>
    {
        Task DeleteAllContributersAsync(Guid chatId);
        Task<List<Contributor>> GetAllAsync(ObjectId chatId);
    }
}
