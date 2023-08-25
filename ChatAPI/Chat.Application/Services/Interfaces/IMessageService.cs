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
        Task<MessageEntity> CreateAsync(MessageDTO gotDTO);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<MessageDTO>> GetAllAsync();
        Task<MessageDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(MessageDTO updateDTO, Guid id);
    }
}
