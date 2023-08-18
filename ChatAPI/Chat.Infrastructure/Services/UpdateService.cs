﻿using AutoMapper;
using Chat.Application.DTOs.Chat;
using Chat.Application.Services.Converters;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Services
{
    public class UpdateService<TEntity, TDTO> : IUpdateService<TEntity, TDTO>
        where TEntity : BaseEntity
        where TDTO : class
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;
        private TDTO _updateDTO { get; set; }
        private ObjectId _objectId { get; }

        public UpdateService(IMapper mapper, IRepository<TEntity> repository, TDTO updateDTO, ObjectId objectId)
        {
            _mapper = mapper;
            _repository = repository;
            _updateDTO = updateDTO;
            _objectId = objectId;
        }

        public async Task<bool> UpdateAsync()
        {
            var oldEntity = await _repository.GetByIdAsync(_objectId);
            if (oldEntity is null)
            {
                return false;
            }
            var updateEntity = _mapper.Map<TEntity>(_updateDTO);
            updateEntity.Id = oldEntity.Id;
            await _repository.UpdateAsync(_objectId, updateEntity);
            return true;

        }
    }
}
