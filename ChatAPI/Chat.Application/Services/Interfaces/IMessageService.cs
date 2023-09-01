using Chat.Application.DTOs.Chat;
using Chat.Application.DTOs.Message;
using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.Interfaces
{
    public interface IMessageService
    {
        public MessageDTO MessageDTO { get; set; }
        Task<Message> CreateAsync(Guid chatId, MessageDTO gotDTO);
        Task DeleteAsync(Guid chatId, Guid id);
        Task<IEnumerable<MessageDTO>> GetAllAsync(Guid chatId);
        Task<MessageDTO> GetByIdAsync(Guid chatId, Guid id);
        Task UpdateAsync(Guid chatId,MessageDTO updateDTO, Guid id);
        Task DeleteAllChatBelongMessagesAsync(Guid chatId);
    }
}
