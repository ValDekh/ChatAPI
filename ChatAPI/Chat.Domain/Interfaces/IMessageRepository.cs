using Chat.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<List<Message>> GetAllAsync(ObjectId chatId);
      //  Task DeleteAllMessagesAsync(Guid chatId);
    }
}
