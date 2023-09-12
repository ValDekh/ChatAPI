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
        public MessageDTOResponse MessageDTO { get; set; }
        Task<Message> CreateAsync(Guid chatId, Guid userId, MessageDTORequest gotDTO);
        Task DeleteAsync(Guid chatId, Guid userId, Guid id);
        Task<IEnumerable<MessageDTOResponse>> GetAllAsync(Guid chatId, Guid userId);
        Task<MessageDTOResponse> GetByIdAsync(Guid chatId, Guid userId, Guid id);
        Task UpdateAsync(Guid chatId, Guid userId, MessageDTORequest updateDTO, Guid id);
        Task<List<MessageDTOResponse>> GetMessagesWithPaginationAsync(Guid chatId, Guid userId, int page);
    }
}
