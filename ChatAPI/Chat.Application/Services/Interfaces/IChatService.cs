using AutoMapper;
using Chat.Application.DTOs.Chat;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.Interfaces
{
    public interface IChatService
    {
        public ChatDTO ChatDTO { get; set; }
        Task<ChatEntity> CreateAsync(ChatDTO gotDTO);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ChatDTO>> GetAllAsync();
        Task<ChatDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(ChatDTO updateDTO, Guid id);
    }
}

