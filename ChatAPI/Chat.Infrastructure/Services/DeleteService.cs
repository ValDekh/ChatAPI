using AutoMapper;
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
    public class DeleteService<TEntity, TDTO> : IDeleteService<TEntity, TDTO>
        where TEntity : BaseEntity
        where TDTO : class
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;
        private ObjectId _objectId { get; }

        public DeleteService(IMapper mapper, IRepository<TEntity> repository, ObjectId objectId)
        {
            _mapper = mapper;
            _repository = repository;
            _objectId = objectId;
        }

        public async Task<bool> DeleteAsync()
        {
            var entity = await _repository.GetByIdAsync(_objectId);
            if (entity is null)
            {
                return false;
            }
            await _repository.DeleteAsync(_objectId);
            return true;
        }
    }
}
