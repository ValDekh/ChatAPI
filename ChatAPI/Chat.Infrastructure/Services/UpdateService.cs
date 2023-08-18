using AutoMapper;
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
using System.Security.Cryptography;
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
        private string _id { get; }

        public UpdateService(IMapper mapper, IRepository<TEntity> repository, TDTO updateDTO, string id)
        {
            _mapper = mapper;
            _repository = repository;
            _updateDTO = updateDTO;
            _id = id;
        }

        public async Task<StatusCodeResult> UpdateAsync()
        {
            if (!ObjectId.TryParse(_id, out ObjectId objectId))
            {
                throw new InvalidDataException("Invalid ObjectId format.");
            }

            var oldEntity = await _repository.GetByIdAsync(objectId);
            if (oldEntity is null)
            {
                return new NotFoundResult();
            }
            var updateEntity = _mapper.Map<TEntity>(_updateDTO);
            updateEntity.Id = oldEntity.Id;
            await _repository.UpdateAsync(objectId, updateEntity);
            return new OkResult();

        }
    }
}
