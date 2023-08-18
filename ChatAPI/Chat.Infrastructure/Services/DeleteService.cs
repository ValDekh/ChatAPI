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
        private string _id { get; }

        public DeleteService(IMapper mapper, IRepository<TEntity> repository, string id)
        {
            _mapper = mapper;
            _repository = repository;
            _id = id;
        }

        public async Task<StatusCodeResult> DeleteAsync()
        {
            if (!ObjectId.TryParse(_id, out ObjectId objectId))
            {
                throw new InvalidDataException("Invalid ObjectId format.");
            }

            var entity = await _repository.GetByIdAsync(objectId);
            if (entity is null)
            {
                return new NotFoundResult(); ;
            }
            await _repository.DeleteAsync(objectId);
            return new NoContentResult();
        }
    }
}
