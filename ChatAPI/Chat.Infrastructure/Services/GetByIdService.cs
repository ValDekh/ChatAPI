using AutoMapper;
using Chat.Application.DTOs.Chat;
using Chat.Application.Services.Converters;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Services
{
    public class GetByIdService<TEntity, TDTO> : IGetByIdService<TEntity, TDTO>
        where TEntity : BaseEntity
        where TDTO : class
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;
        private readonly string _id;
        public GetByIdService(IMapper mapper, IRepository<TEntity> repository, string id)
        {
            _mapper = mapper;
            _repository = repository;
            _id = id;
        }

        public async Task<TDTO> GetByIdAsync()
        {
            if (!ObjectId.TryParse(_id, out ObjectId objectId))
            {
               throw new InvalidDataException("Invalid ObjectId format.");
            }
            var entity = await _repository.GetByIdAsync(objectId);
            var gotDTO = _mapper.Map<TDTO>(entity);

            return gotDTO;
        }


    }
}
