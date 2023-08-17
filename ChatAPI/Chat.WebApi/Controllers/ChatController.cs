﻿using Chat.Infrastructure.DataAccess.Contexts;
using Chat.Infrastructure.Repositories;
using Chat.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using AutoMapper;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;
using System;
using Chat.Domain.Interfaces;
using Chat.Application.DTOs.Chat;
using Chat.Infrastructure.DataAccess;
using Chat.Infrastructure.Factory;
using Chat.Infrastructure.Services;

namespace Chat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly IRepository<ChatEntity> _chatService;
        private readonly IMapper _mapper;

        public ChatController(DbSetting dbSetting, IMapper mapper)
        {
            _chatService = new MongoRepositoryFactory(dbSetting).CreateRepository<ChatEntity>("chatCollection");
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var getAllService = new GetAllService<ChatEntity, ChatDTO>(_mapper, _chatService);
            var gotChatDTO = await getAllService.GetAllAsync();
            return Ok(gotChatDTO);
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var getByIdService = new GetByIdService<ChatEntity, ChatDTO>(_mapper, _chatService, id);
            var gotChatDTO = await getByIdService.GetByIdAsync();
            if (gotChatDTO == null)
            {
                return NotFound();
            }
            return Ok(gotChatDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync (ChatDTO newChatDTO)
        {
            var createService = new CreateService<ChatEntity,ChatDTO>(_mapper, _chatService, newChatDTO);
            var chatEntity = await createService.CreateAsync();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = chatEntity.Id }, chatEntity);
        }


        [HttpPut("{chatId:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid chatId, ChatDTO updateChatDTO)
        {
            var updateService = new UpdateService<ChatEntity, ChatDTO>(_mapper, _chatService, updateChatDTO, chatId);
            var isUpdateSucces = await updateService.UpdateAsync();
            if (!isUpdateSucces)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deleteService = new DeleteService<ChatEntity, ChatDTO>(_mapper, _chatService, id);
            var isDeleteSucces = await deleteService.DeleteAsync();
            if (!isDeleteSucces)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
