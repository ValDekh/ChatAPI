﻿using AutoMapper;
using Chat.Application.DTOs.Contributer;
using Chat.Application.DTOs.Message;
using Chat.Application.Services.Converters;
using Chat.Domain.Entities;
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
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Services
{
    public class ContributorService
    {
        private readonly IMapper _mapper;
        private readonly IChatRepository _chatRepository;
        private readonly IContributorRepository _contributorRepository;
        public ContributorDTOResponse ContributorDTOResponse { get; set; }

        public ContributorService(IMapper mapper, IChatRepository chatRepository, IContributorRepository contributorRepository)
        {
            _mapper = mapper;
            _chatRepository = chatRepository;
            _contributorRepository = contributorRepository;
        }

        public async Task<Contributor> AddContributorAsync(Guid chatId, Guid ownerId, ContributorDTORequest contributorDTORequest )
        {
            await ChatExistAsync(chatId);
            var objectIdOwner = ObjectIdGuidConverter.ConvertGuidToObjectId(ownerId);
            var objectIdChat = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            var contributor =
                await _contributorRepository.FindAsync(x => x.UserId == objectIdOwner && x.ChatId == objectIdChat);
            if (contributor == null)
            {
                throw new ContributorNotFoundException(ownerId);
            }

            if (!PermissionHelper.HasPermission(contributor.Permissions, Permissions.AddContributor))
            {
                throw ForbiddenException.Default(ownerId);
            }

            var newContributor = _mapper.Map<Contributor>(contributorDTORequest);
            newContributor.ChatId = objectIdChat;
            
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
            var contributor = await _contributorRepository.FindAsync(x => x.ChatId == objectIdChat && x.UserId == objectIdOwner);
            if (contributor == null)
            {
                throw new ContributorNotFoundException(ownerId);
            }

            if (!PermissionHelper.HasPermission(contributor.Permissions, Permissions.DeleteContributor))
            {
                throw ForbiddenException.Default(ownerId);
            }
            var entity = await _contributorRepository.GetByIdAsync(objectIdUser);
            if (entity is null)
            {
                throw new MessageNotFoundException(userId);
            }

            await _contributorRepository.DeleteAsync(entity.Id);
        }

        public async Task<IEnumerable<ContributorDTOResponse>> GetAllAsync(Guid chatId, Guid ownerId)
        {
            await ChatExistAsync(chatId);

            var objectIdOwner = ObjectIdGuidConverter.ConvertGuidToObjectId(ownerId);
            var objectIdChat = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            var contributor = await _contributorRepository.FindAsync(x => x.ChatId == objectIdChat && x.UserId == objectIdOwner);
            if (contributor == null)
            {
                throw new ContributorNotFoundException(ownerId);
            }
            if (!PermissionHelper.HasPermission(contributor.Permissions, Permissions.ReadContributor))
            {
                throw ForbiddenException.Default(ownerId);
            }

            var contributorEntities = await _contributorRepository.GetAllAsync(objectIdChat);
            var contributorDTOsResponse = _mapper.Map<IEnumerable<ContributorDTOResponse>>(contributorEntities);
            return contributorDTOsResponse;
        }

        public async Task<ContributorDTOResponse> GetByIdAsync(Guid chatId, Guid ownerId, Guid userId)
        {
            await ChatExistAsync(chatId);
            
            var objectIdOwner = ObjectIdGuidConverter.ConvertGuidToObjectId(ownerId);
            var objectIdChat = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            var objectIdUser = ObjectIdGuidConverter.ConvertGuidToObjectId(userId);
            var contributor = await _contributorRepository.FindAsync(x => x.ChatId == objectIdChat && x.UserId == objectIdOwner);
            if (contributor == null)
            {
                throw new ContributorNotFoundException(ownerId);
            }
            if (!PermissionHelper.HasPermission(contributor.Permissions, Permissions.ReadMessage))
            {
                throw ForbiddenException.Default(ownerId);
            }

            var entity = await _contributorRepository.GetByIdAsync(objectIdUser);
            if (entity is null)
            {
                throw new ContributorNotFoundException(ownerId);
            }

            var contributorDTOResponce = _mapper.Map<ContributorDTOResponse>(entity);
            return contributorDTOResponce;
        }

        public async Task UpdateAsync(Guid chatId, Guid ownerId, Guid userId, ContributorDTORequest newPermissionForExistContrib, Guid id)
        {
            await ChatExistAsync(chatId);
            ObjectId objectId = ObjectIdGuidConverter.ConvertGuidToObjectId(id);
            if (!ObjectId.TryParse(objectId.ToString(), out _))
            {
                throw new InvalidDataException("Invalid format.");
            }
            var objectIdUser = ObjectIdGuidConverter.ConvertGuidToObjectId(userId);
            var objectIdChat = ObjectIdGuidConverter.ConvertGuidToObjectId(chatId);
            var contributor = await _contributorRepository.FindAsync(x => x.ChatId == objectIdChat && x.UserId == objectIdUser);
            if (contributor == null)
            {
                throw new ContributorNotFoundException(userId);
            }
            if (!PermissionHelper.HasPermission(contributor.Permissions, Permissions.UpdateMessage))
            {
                throw ForbiddenException.Default(userId);
            }
            var oldEntity = await _messageRepository.GetByIdAsync(objectId);
            if (oldEntity is null)
            {
                throw new MessageNotFoundException(id);
            }
            var updateEntity = _mapper.Map<Message>(newPermissionForExistContrib);
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
