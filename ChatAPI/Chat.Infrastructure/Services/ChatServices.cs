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
    public class ChatServices<TEntity, TDTO> : IChatServices<TEntity, TDTO>
        where TEntity : BaseEntity
        where TDTO : class
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;
        public TDTO ChatDTO { get; set; }
        public ChatServices(IMapper mapper, IRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<TEntity> CreateAsync(TDTO gotDTO)
        {
            var newEntity = _mapper.Map<TEntity>(gotDTO);
            await _repository.AddAsync(newEntity);
            ChatDTO = _mapper.Map<TDTO>(newEntity);
            return newEntity;
        }


        public async Task<StatusCodeResult> DeleteAsync(Guid id)
        {
                //if (!ObjectId.TryParse(_id, out ObjectId objectId))
                //{
                //    throw new InvalidDataException("Invalid ObjectId format.");
                //}
                ObjectId objectId = ObjectIdGuidConverter.ConvertGuidToObjectId(id);
                var entity = await _repository.GetByIdAsync(objectId);
                if (entity is null)
                {
                    return new NotFoundResult(); ;
                }
                await _repository.DeleteAsync(objectId);
                return new NoContentResult();
        }

        public async Task<IEnumerable<TDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var gotDTO = _mapper.Map<IEnumerable<TDTO>>(entities);
            return gotDTO;
        }

        public async Task<TDTO> GetByIdAsync(Guid id)
        {
            ObjectId objectId = ObjectIdGuidConverter.ConvertGuidToObjectId(id);
            //if (!ObjectId.TryParse(_id, out ObjectId objectId))
            //{
            //   throw new InvalidDataException("Invalid ObjectId format.");
            //}
            var entity = await _repository.GetByIdAsync(objectId);
            var gotDTO = _mapper.Map<TDTO>(entity);
            return gotDTO;
        }

        public async Task<StatusCodeResult> UpdateAsync(TDTO updateDTO, Guid id)
        {
            ObjectId objectId = ObjectIdGuidConverter.ConvertGuidToObjectId(id);

            var oldEntity = await _repository.GetByIdAsync(objectId);
            if (oldEntity is null)
            {
                return new NotFoundResult();
            }
            var updateEntity = _mapper.Map<TEntity>(updateDTO);
            updateEntity.Id = oldEntity.Id;
            await _repository.UpdateAsync(objectId, updateEntity);
            return new OkResult();
        }
    }
}
