using AutoMapper;
using Chat.Application.DTOs.Chat;
using Chat.Application.EventHandlers.ChatEventHandlers;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.Interfaces
{
    public interface IChatService
    {
        public ChatDTOResponse ChatDTOResponse { get; set; }
        Task<ChatEntity> CreateAsync(ChatDTORequest gotDTO);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ChatDTOResponse>> GetAllAsync();
        Task<ChatDTOResponse> GetByIdAsync(Guid id);
        Task UpdateAsync(ChatDTORequest updateDTO, Guid id);
    }
}

