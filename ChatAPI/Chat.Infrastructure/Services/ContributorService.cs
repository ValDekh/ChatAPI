using AutoMapper;
using Chat.Application.DTOs.Contributer;
using Chat.Application.DTOs.Message;
using Chat.Application.Services.Converters;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Exceptions.ContributorExist;
using Chat.Domain.Exceptions.ForbiddenException;
using Chat.Domain.Exceptions.NotFound;
using Chat.Domain.Helpers;
using Chat.Domain.Interfaces;
using Chat.Domain.Structures;
using Chat.Infrastructure.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Services
{
    public class ContributorService : IContributorService
    {
        private readonly IMapper _mapper;
        private readonly IChatRepository _chatRepository;
        private readonly IContributorRepository _contributorRepository;
        public ContributorDTOResponse ContributorDTOResponse { get; set; }

        public ContributorService(IMapper mapper,
            IChatRepository chatRepository,
            IContributorRepository contributorRepository)
        {
            _mapper = mapper;
            _chatRepository = chatRepository;
            _contributorRepository = contributorRepository;
        }

        public async Task<Contributor> AddContributorAsync(
            Guid chatId,
            Guid ownerId,
            ContributorDTORequest contributorDTORequest)
        {
            await ChatExistAsync(chatId);
            var objectIdOwner = ObjectIdGuidConverter.ConvertGuidToObjectId(ownerId);
            var objectIdChat = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            await IsUserExistInACurrentChat(objectIdChat, ObjectIdGuidConverter.ConvertGuidToObjectId(contributorDTORequest.UserId));
            await IsOwnerHasPermission(objectIdChat, objectIdOwner, Permissions.AddContributor);

            var newContributor = _mapper.Map<Contributor>(contributorDTORequest);

            await IsUserAlreadyHasContributor(newContributor.UserId);
            
            newContributor.ChatId = objectIdChat;
            newContributor.CreatedBy = objectIdOwner;

            await _contributorRepository.AddAsync(newContributor);
            ContributorDTOResponse = _mapper.Map<ContributorDTOResponse>(newContributor);
            return newContributor;
        }

        public async Task DeleteAsync(Guid chatId, Guid ownerId, Guid userId)
        {
            await ChatExistAsync(chatId);

            var objectIdOwner = ObjectIdGuidConverter.ConvertGuidToObjectId(ownerId);
            var objectIdChat = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            var objectIdUser = ObjectIdGuidConverter.ConvertGuidToObjectId(userId);
            await IsUserExistInACurrentChat(objectIdChat, objectIdUser);
            await IsOwnerHasPermission(objectIdChat, objectIdOwner, Permissions.DeleteContributor);
            
            var entity = await _contributorRepository.FindAsync(x => x.ChatId == objectIdChat && x.UserId == objectIdUser);
            if (entity is null)
            {
                throw new ContributorNotFoundException(userId);
            }

            await _contributorRepository.DeleteAsync(entity.Id);
        }

        public async Task<IEnumerable<ContributorDTOResponse>> GetAllAsync(Guid chatId, Guid ownerId)
        {
            await ChatExistAsync(chatId);

            var objectIdOwner = ObjectIdGuidConverter.ConvertGuidToObjectId(ownerId);
            var objectIdChat = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            await IsOwnerHasPermission(objectIdChat, objectIdOwner, Permissions.ReadContributor);

            var contributorEntities = await _contributorRepository.GetAllAsync(objectIdChat);
            var contributorDTOsResponse = _mapper.Map<IEnumerable<ContributorDTOResponse>>(contributorEntities);
            return contributorDTOsResponse;
        }

        public async Task<ContributorDTOResponse> GetByUserIdAsync(Guid chatId, Guid ownerId, Guid userId)
        {
            await ChatExistAsync(chatId);

            var objectIdOwner = ObjectIdGuidConverter.ConvertGuidToObjectId(ownerId);
            var objectIdChat = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            var objectIdUser = ObjectIdGuidConverter.ConvertGuidToObjectId(userId);
            await IsUserExistInACurrentChat(objectIdChat, objectIdUser);
            await IsOwnerHasPermission(objectIdChat, objectIdOwner, Permissions.ReadContributor);

            var entity = await _contributorRepository.FindAsync(x => x.ChatId == objectIdChat && x.UserId == objectIdUser);
            if (entity is null)
            {
                throw new ContributorNotFoundException(userId);
            }

            var contributorDTOResponce = _mapper.Map<ContributorDTOResponse>(entity);
            return contributorDTOResponce;
        }

        public async Task UpdateAsync(Guid chatId,
            Guid ownerId, ContributorDTORequest newPermissionForExistContrib)
        {
            await ChatExistAsync(chatId);

            var objectIdOwner = ObjectIdGuidConverter.ConvertGuidToObjectId(ownerId);
            var objectIdChat = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            var objectIdUser = ObjectIdGuidConverter.ConvertGuidToObjectId(newPermissionForExistContrib.UserId);
            await IsUserExistInACurrentChat(objectIdChat, objectIdUser);
            await IsOwnerHasPermission(objectIdChat, objectIdOwner, Permissions.UpdateContributor);

            var oldEntity = await _contributorRepository.FindAsync(x => x.ChatId == objectIdChat &&
                   x.UserId == objectIdUser);
            if (oldEntity is null)
            {
                throw new ContributorNotFoundException(newPermissionForExistContrib.UserId);
            }
            var updateEntity = _mapper.Map<Contributor>(newPermissionForExistContrib);
            updateEntity.Id = oldEntity.Id;
            updateEntity.ChatId = oldEntity.ChatId;
            updateEntity.UpdatedBy = objectIdUser;
            await _contributorRepository.UpdateAsync(updateEntity.Id, updateEntity);
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

        private async Task IsUserExistInACurrentChat(ObjectId chatId, ObjectId userId)
        {

            var chat = await _chatRepository.GetByIdAsync(chatId);
            var isUserExist = chat.Users.Contains(userId);
            if (!isUserExist)
            {
                throw new UserNotFoundException(ObjectIdGuidConverter.ConvertObjectIdToGuid(chatId));
            }
        }

        private async Task IsOwnerHasPermission (ObjectId chatId, ObjectId ownerId, string action)
        {
            var contributor = await _contributorRepository
               .FindAsync(x => x.ChatId == chatId && x.UserId == ownerId);
            if (contributor == null)
            {
                throw new ContributorNotFoundException(ObjectIdGuidConverter.ConvertObjectIdToGuid(ownerId));
            }
            if (!PermissionHelper.HasPermission(contributor.Permissions, action))
            {
                throw ForbiddenException.Default(ObjectIdGuidConverter.ConvertObjectIdToGuid(ownerId));
            }
        }

        private async Task IsUserAlreadyHasContributor(ObjectId userId)
        {
            var contributer = await _contributorRepository.FindAsync(x => x.UserId == userId);
            if (contributer is not null)
            {
                throw new ContributorExistException(ObjectIdGuidConverter.ConvertObjectIdToGuid(userId));
            }
        }
    }
}
