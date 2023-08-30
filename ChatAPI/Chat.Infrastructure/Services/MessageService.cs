using AutoMapper;
using Chat.Application.DTOs.Chat;
using Chat.Application.DTOs.Message;
using Chat.Application.Services.Converters;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Context;
using Chat.Domain.Entities;
using Chat.Domain.Exceptions;
using Chat.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Services
{
    public class MessageService : IMessageService

    {
        private readonly IMapper _mapper;
        private readonly IRepository<Message> _repository;
        private readonly IMongoRepositoryFactory _mongoRepositoryFactory;
        private readonly IChatService _chatService;
        public MessageDTO MessageDTO { get; set; }
        public MessageService(IMapper mapper, IMongoRepositoryFactory mongoRepositoryFactory, IChatService chatService)
        {
            _mapper = mapper;
            _mongoRepositoryFactory = mongoRepositoryFactory;
            _repository = _mongoRepositoryFactory.Repository<Message>("messageCollection");
            _chatService = chatService;
        }

        public async Task<Message> CreateAsync(Guid chatId, MessageDTO gotDTO)
        {
            var chat = await ChatExist(chatId);
            var newEntity = _mapper.Map<Message>(gotDTO);
            newEntity.ChatId = chat.Id;
            await _repository.AddAsync(newEntity);
            MessageDTO = _mapper.Map<MessageDTO>(newEntity);
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
                throw new MessageNotFoundException(id);
            }
            await _repository.DeleteAsync(objectId);
        }

        public async Task<IEnumerable<MessageDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var gotDTO = _mapper.Map<IEnumerable<MessageDTO>>(entities);
            return gotDTO;
        }

        public async Task<MessageDTO> GetByIdAsync(Guid id)
        {
            ObjectId objectId = ObjectIdGuidConverter.ConvertGuidToObjectId(id);
            if (!ObjectId.TryParse(objectId.ToString(), out _))
            {
                throw new InvalidDataException("Invalid format.");
            }
            var entity = await _repository.GetByIdAsync(objectId);
            if (entity is null)
            {
                throw new MessageNotFoundException(id);
            }
            var gotDTO = _mapper.Map<MessageDTO>(entity);
            return gotDTO;
        }

        public async Task UpdateAsync(MessageDTO updateDTO, Guid id)
        {
            ObjectId objectId = ObjectIdGuidConverter.ConvertGuidToObjectId(id);
            if (!ObjectId.TryParse(objectId.ToString(), out _))
            {
                throw new InvalidDataException("Invalid format.");
            }
            var oldEntity = await _repository.GetByIdAsync(objectId);
            if (oldEntity is null)
            {
                throw new MessageNotFoundException(id);
            }
            var updateEntity = _mapper.Map<Message>(updateDTO);
            updateEntity.Id = oldEntity.Id;
            await _repository.UpdateAsync(objectId, updateEntity);
        }


        //private async Task<ChatEntity> ChatExist(Guid chatId)
        //{
        //    ObjectId chatIdEntity = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
        //    var collection = _database.GetCollection<ChatEntity>("chatCollection");
        //    var filter = Builders<ChatEntity>.Filter.Eq(x => x.Id, chatIdEntity);
        //    var chat = await collection.Find(filter).FirstOrDefaultAsync();
        //    if (chat is null)
        //    {
        //        throw new ChatNotFoundException(chatId);
        //    }

        //    return chat;
        //}
    }
}
