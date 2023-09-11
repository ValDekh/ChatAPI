using AutoMapper;
using Chat.Application.DTOs.Chat;
using Chat.Application.EventHandlers.ChatEventHandlers;
using Chat.Application.EventHandlers.ContributorCreateEventHandlers;
using Chat.Application.Services.Converters;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Exceptions;
using Chat.Domain.Exceptions.NotFound;
using Chat.Domain.Interfaces;
using MongoDB.Bson;

namespace Chat.Infrastructure.Services
{
    public class ChatService : IChatService

    {
        private readonly IMapper _mapper;
        private readonly IChatRepository _repository;
        private readonly ChatDeletedEventHandler _chatDeletedEvent;
        private readonly ContributorCreatedEventHandler _contributorCreatedEventHandler;

        public ChatDTOResponse ChatDTOResponse { get; set; }
        public ChatService(IMapper mapper,
            IChatRepository chatRepository,
            ChatDeletedEventHandler chatDeletedEventHandler,
            ContributorCreatedEventHandler contributorCreatedEventHandler)
        {
            _mapper = mapper;
            _repository = chatRepository;
            _chatDeletedEvent = chatDeletedEventHandler;
            _contributorCreatedEventHandler = contributorCreatedEventHandler;
        }

        public async Task<ChatEntity> CreateAsync(ChatDTORequest ChatDTORequest)
        {
            var newEntity = _mapper.Map<ChatEntity>(ChatDTORequest);
            await _repository.AddAsync(newEntity);
            var userChatOwnerId = newEntity.Users.FirstOrDefault();
            _contributorCreatedEventHandler.CreateInvoke(new ContributorCreateEventArgs()
            {
                ChatId = newEntity.Id,
                UserId = userChatOwnerId
            });
            ChatDTOResponse = _mapper.Map<ChatDTOResponse>(newEntity);
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
            _chatDeletedEvent?.CreateInvoke(new ChatDeletedEventArgs(id));
            await _repository.DeleteAsync(objectId);
        }

        public async Task<IEnumerable<ChatDTOResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var gotDTO = _mapper.Map<IEnumerable<ChatDTOResponse>>(entities);
            return gotDTO;
        }

        public async Task<ChatDTOResponse> GetByIdAsync(Guid id)
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
            var gotDTO = _mapper.Map<ChatDTOResponse>(entity);
            return gotDTO;
        }

        public async Task UpdateAsync(ChatDTORequest updateDTO, Guid id)
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
