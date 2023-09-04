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

namespace Chat.Infrastructure.Services
{
    public class MessageService : IMessageService

    {
        private readonly IMapper _mapper;
        private readonly IMessageRepository _repository;
        private readonly IChatRepository _chatRepository;
        private readonly IMongoRepositoryAndCollectionFactory _mongoRepositoryAndCollectionFactory;
        public MessageDTOResponse MessageDTO { get; set; }
        public MessageService(IMapper mapper, IMongoRepositoryAndCollectionFactory mongoRepositoryFactory)
        {
            _mapper = mapper;
            _mongoRepositoryAndCollectionFactory = mongoRepositoryFactory;
            _repository = new MessageRepository(_mongoRepositoryAndCollectionFactory.GetExistOrNewCollection<Message>("messageCollection"));
            _chatRepository = new ChatRepository(_mongoRepositoryAndCollectionFactory.GetExistOrNewCollection<ChatEntity>("chatCollection"));
        }

        public async Task<Message> CreateAsync(Guid chatId, MessageDTORequest gotDTO)
        {
            await ChatExistAsync(chatId);
            var newEntity = _mapper.Map<Message>(gotDTO);
            newEntity.ChatId = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            await _repository.AddAsync(newEntity);
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
            var entity = await _repository.GetByIdAsync(objectId);
            if (entity is null)
            {
                throw new MessageNotFoundException(id);
            }
            await _repository.DeleteAsync(objectId);
        }

        //TODO (Create a pagination here)
        public async Task<IEnumerable<MessageDTOResponse>> GetAllAsync(Guid chatId)
        {
            ObjectId chatObjectId = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            if (!ObjectId.TryParse(chatObjectId.ToString(), out _))
            {
                throw new InvalidDataException("Invalid format.");
            }
            var entities = await _repository.GetAllAsync(chatObjectId);
            await ChatExistAsync(chatId);
            var gotDTO = _mapper.Map<IEnumerable<MessageDTOResponse>>(entities);
            return gotDTO;
        }

        public async Task<MessageDTOResponse> GetByIdAsync(Guid chatId,Guid id)
        {
            await ChatExistAsync(chatId);
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
            var oldEntity = await _repository.GetByIdAsync(objectId);
            if (oldEntity is null)
            {
                throw new MessageNotFoundException(id);
            }
            var updateEntity = _mapper.Map<Message>(updateDTO);
            updateEntity.Id = oldEntity.Id;
            await _repository.UpdateAsync(objectId, updateEntity);
        }

        //public async Task DeleteAllMessagesAsync(Guid chatId)
        //{
        //    await ChatExistAsync(chatId);
        //    ObjectId chatIdEntity = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
        //    var listWrites = new List<WriteModel<Message>>();
        //    var filterDefinition = Builders<Message>.Filter.Eq(p => p.ChatId, chatIdEntity);
        //    listWrites.Add(new DeleteManyModel<Message>(filterDefinition));
        //    await _collection.BulkWriteAsync(listWrites);
        //}

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
