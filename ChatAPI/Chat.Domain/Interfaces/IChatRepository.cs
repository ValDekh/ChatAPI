using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Interfaces
{
    public interface IChatRepository : IRepository<ChatEntity>
    {
        Task<List<ChatEntity>> GetAllAsync();
    }
}
