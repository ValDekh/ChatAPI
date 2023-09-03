using AutoMapper;
using Chat.Application.DTOs.Chat;
using Chat.Application.EventHandlers.ChatEventHandler;
using Chat.Application.Services.Converters;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Context;
using Chat.Domain.Entities;
using Chat.Domain.Exceptions;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.Factories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Services
{
    public class ChatService : IChatService
      
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ChatEntity> _repository;
        private readonly IMongoCollection<ChatEntity> _chatCollection;
        private readonly IMongoRepositoryAndCollectionFactory _mongoRepositoryAndCollectionFactory;
        public event EventHandler<ChatDeletedEventArgs> ChatDeleted;
        public ChatDTO ChatDTO { get; set; }
        public ChatService(IMapper mapper,IMongoRepositoryAndCollectionFactory mongoRepositoryFactory, IMessageService messageService)
        {
            _mapper = mapper;
            _mongoRepositoryAndCollectionFactory = mongoRepositoryFactory;
            _repository = _mongoRepositoryAndCollectionFactory.Repository<ChatEntity>("chatCollection");
            _chatCollection = _mongoRepositoryAndCollectionFactory.GetExistCollection<ChatEntity>("chatCollection");
        }

        
        public async Task<ChatEntity> CreateAsync(ChatDTO gotDTO)
        {
            var newEntity = _mapper.Map<ChatEntity>(gotDTO);
            await _repository.AddAsync(newEntity);
            ChatDTO = _mapper.Map<ChatDTO>(newEntity);
            return newEntity;
        }


        public async Task DeleteAsync(Guid id)
        {

            ObjectId objectId = ObjectIdGuidConverter.ConvertGuidToObjectId(id);
            if (!ObjectId.TryParse(objectId.ToString(), out _))
            {
                throw new InvalidDataException("Invalid format.");
            }
            var entity = await _repository.GetByIdAsync(objectId);
            if (entity is null)
            {
                throw new ChatNotFoundException(id);
            }
            ChatDeleted?.Invoke(this, new ChatDeletedEventArgs(id));        
            await _repository.DeleteAsync(objectId);
        }

        public async Task<IEnumerable<ChatDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var gotDTO = _mapper.Map<IEnumerable<ChatDTO>>(entities);
            return gotDTO;
        }

        public async Task<ChatDTO> GetByIdAsync(Guid id)
        {
            ObjectId objectId = ObjectIdGuidConverter.ConvertGuidToObjectId(id);
            if (!ObjectId.TryParse(objectId.ToString(), out _))
            {
                throw new InvalidDataException("Invalid format.");
            }
            var entity = await _repository.GetByIdAsync(objectId);
            if (entity is null)
                {
                    throw new ChatNotFoundException(id);
                }
            var gotDTO = _mapper.Map<ChatDTO>(entity);
            return gotDTO;
        }

        public async Task UpdateAsync(ChatDTO updateDTO, Guid id)
        {
            ObjectId objectId = ObjectIdGuidConverter.ConvertGuidToObjectId(id);
            if (!ObjectId.TryParse(objectId.ToString(), out _))
                {
                    throw new InvalidDataException("Invalid format.");
                }
            var oldEntity = await _repository.GetByIdAsync(objectId);
            if (oldEntity is null)
                {
                    throw new ChatNotFoundException(id);
                }
            var updateEntity = _mapper.Map<ChatEntity>(updateDTO);
            updateEntity.Id = oldEntity.Id;
            await _repository.UpdateAsync(objectId, updateEntity);
        }
    }
}
