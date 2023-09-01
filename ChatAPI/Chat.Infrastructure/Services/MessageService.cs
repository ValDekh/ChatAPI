using AutoMapper;
using Chat.Application.DTOs.Message;
using Chat.Application.Services.Converters;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Exceptions;
using Chat.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Chat.Infrastructure.Services
{
    public class MessageService : IMessageService

    {
        private readonly IMapper _mapper;
        private readonly IRepository<Message> _repository;
        private readonly IMongoCollection<Message> _collection;
        private readonly IMongoRepositoryAndCollectionFactory _mongoRepositoryAndCollectionFactory;
        private readonly IChatService _chatService;
        public MessageDTO MessageDTO { get; set; }
        public MessageService(IMapper mapper, IMongoRepositoryAndCollectionFactory mongoRepositoryFactory, IChatService chatService)
        {
            _mapper = mapper;
            _mongoRepositoryAndCollectionFactory = mongoRepositoryFactory;
            _repository = _mongoRepositoryAndCollectionFactory.Repository<Message>("messageCollection");
            _collection = _mongoRepositoryAndCollectionFactory.GetExistCollection<Message>("messageCollection");
            _chatService = chatService;
        }

        public async Task<Message> CreateAsync(Guid chatId, MessageDTO gotDTO)
        {
            await ChatExistAsync(chatId);
            gotDTO.ChatId = chatId;
            var newEntity = _mapper.Map<Message>(gotDTO);
            await _repository.AddAsync(newEntity);
            await _chatService.UpdateMassageIdListAsync(chatId, newEntity.Id);
            MessageDTO = _mapper.Map<MessageDTO>(newEntity);
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
            await _chatService.DeleteMassageIdListAsync(chatId,objectId);
            await _repository.DeleteAsync(objectId);
        }

        //TODO (Create a pagination here)
        public async Task<IEnumerable<MessageDTO>> GetAllAsync(Guid chatId)
        {
            await ChatExistAsync(chatId);
            var entities = await _repository.GetAllAsync();
            var gotDTO = _mapper.Map<IEnumerable<MessageDTO>>(entities);
            return gotDTO;
        }

        public async Task<MessageDTO> GetByIdAsync(Guid chatId,Guid id)
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
            var gotDTO = _mapper.Map<MessageDTO>(entity);
            return gotDTO;
        }

        public async Task UpdateAsync(Guid chatId, MessageDTO updateDTO, Guid id)
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

        public async Task DeleteAllChatBelongMessagesAsync(Guid chatId)
        {
            await ChatExistAsync(chatId);
            ObjectId chatIdEntity = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            var listWrites = new List<WriteModel<Message>>();
            var filterDefinition = Builders<Message>.Filter.Eq(p => p.ChatId, chatIdEntity);
            listWrites.Add(new DeleteManyModel<Message>(filterDefinition));
            await _collection.BulkWriteAsync(listWrites);
        }

        private async Task ChatExistAsync(Guid chatId)
        {
            var chat = await _chatService.GetByIdAsync(chatId);
            if (chat is null)
            {
                throw new ChatNotFoundException(chatId);
            }
        }
    }
}
