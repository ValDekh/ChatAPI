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
        private Guid _guid { get; }

        public DeleteService(IMapper mapper, IRepository<TEntity> repository, Guid guid)
        {
            _mapper = mapper;
            _repository = repository;
            _guid = guid;
        }

        public async Task<bool> DeleteAsync()
        {
            var objectId = ObjectIdGuidConverter.ConvertGuidToObjectId(_guid);
            var entity = await _repository.GetByIdAsync(objectId);
            if (entity is null)
            {
                return false;
            }
            await _repository.DeleteAsync(objectId);
            return true;
        }
    }
}
