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
    public interface IChatServices<TEntity,TDTO>
    {
        Task<TEntity> CreateAsync(TDTO gotDTO);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<TDTO>> GetAllAsync();
        Task<TDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(TDTO updateDTO, Guid id);
    }
}

