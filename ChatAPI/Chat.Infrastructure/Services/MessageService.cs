using AutoMapper;
using Chat.Application.DTOs.Message;
using Chat.Application.Services.Converters;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Exceptions;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace Chat.Infrastructure.Services
{
    public class MessageService : IMessageService

    {
        private readonly IMapper _mapper;
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRepository _chatRepository;
        public MessageDTOResponse MessageDTO { get; set; }
        public MessageService(IMapper mapper,
            IMongoCollectionFactory mongoRepositoryFactory,
            IMessageRepository messageRepository,
            IChatRepository chatRepository)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
        }

        public async Task<Message> CreateAsync(Guid chatId, MessageDTORequest gotDTO)
        {
            await ChatExistAsync(chatId);
            var newEntity = _mapper.Map<Message>(gotDTO);
            newEntity.ChatId = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            await _messageRepository.AddAsync(newEntity);
            MessageDTO = _mapper.Map<MessageDTOResponse>(newEntity);
            return newEntity;
        }

        public async Task DeleteAsync(Guid chatId, Guid id)
        {
            await ChatExistAsync(chatId);
            ObjectId objectId = ObjectIdGuidConverter.ConvertGuidToObjectId(id);
            if (!ObjectId.TryParse(objectId.ToString(), out _))
            {
                throw new InvalidDataException("Invalid format.");
            }
            var entity = await _messageRepository.GetByIdAsync(objectId);
            if (entity is null)
            {
                throw new MessageNotFoundException(id);
            }
            await _messageRepository.DeleteAsync(objectId);
        }

        public async Task<List<MessageDTOResponse>> GetMessagesWithPaginationAsync(Guid chatId, int page)
        {
            const int pageSize = 10;
            await ChatExistAsync(chatId);
            ObjectId objectId = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            if (!ObjectId.TryParse(objectId.ToString(), out _))
            {
                throw new InvalidDataException("Invalid format.");
            }
            var skip = (page - 1) * pageSize;
            var messages = await _messageRepository.GetMessagesWithPaginationAsync(objectId, skip, pageSize);
            var messageDTOs = _mapper.Map<List<MessageDTOResponse>>(messages);
            return messageDTOs;
        }

        public async Task<IEnumerable<MessageDTOResponse>> GetAllAsync(Guid chatId)
        {
            ObjectId chatObjectId = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            if (!ObjectId.TryParse(chatObjectId.ToString(), out _))
            {
                throw new InvalidDataException("Invalid format.");
            }
            var entities = await _messageRepository.GetAllAsync(chatObjectId);
            await ChatExistAsync(chatId);
            var gotDTO = _mapper.Map<IEnumerable<MessageDTOResponse>>(entities);
            return gotDTO;
        }

        public async Task<MessageDTOResponse> GetByIdAsync(Guid chatId, Guid id)
        {
            await ChatExistAsync(chatId);
            ObjectId objectId = ObjectIdGuidConverter.ConvertGuidToObjectId(id);
            if (!ObjectId.TryParse(objectId.ToString(), out _))
            {
                throw new InvalidDataException("Invalid format.");
            }
            var entity = await _messageRepository.GetByIdAsync(objectId);
            if (entity is null)
            {
                throw new MessageNotFoundException(id);
            }
            var gotDTO = _mapper.Map<MessageDTOResponse>(entity);
            return gotDTO;
        }

        public async Task UpdateAsync(Guid chatId, MessageDTORequest updateDTO, Guid id)
        {
            await ChatExistAsync(chatId);
            ObjectId objectId = ObjectIdGuidConverter.ConvertGuidToObjectId(id);
            if (!ObjectId.TryParse(objectId.ToString(), out _))
            {
                throw new InvalidDataException("Invalid format.");
            }
            var oldEntity = await _messageRepository.GetByIdAsync(objectId);
            if (oldEntity is null)
            {
                throw new MessageNotFoundException(id);
            }
            var updateEntity = _mapper.Map<Message>(updateDTO);
            updateEntity.Id = oldEntity.Id;
            updateEntity.ChatId = oldEntity.ChatId;
            await _messageRepository.UpdateAsync(objectId, updateEntity);
        }

        private async Task ChatExistAsync(Guid chatId)
        {
            ObjectId chatObjectId = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            if (!ObjectId.TryParse(chatObjectId.ToString(), out _))
            {
                throw new InvalidDataException("Invalid format.");
            }
            var chat = await _chatRepository.GetByIdAsync(chatObjectId);
            if (chat is null)
            {
                throw new ChatNotFoundException(chatId);
            }
        }
    }
}
