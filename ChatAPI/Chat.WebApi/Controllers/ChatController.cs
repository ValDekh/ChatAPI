using Chat.Infrastructure.DataAccess.Contexts;
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
            var newChat = _mapper.Map<ChatEntity>(newChatDTO);
            await _chatService.AddAsync(newChat);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = newChat.Id }, newChat);
        }


        [HttpPut("{chatId:length(24)}")]
        public async Task<IActionResult> Update([FromRoute] string chatId, [FromBody] ChatDTO updateChatDTO)
        {
            if (!ObjectId.TryParse(chatId, out ObjectId objectId))
            {
                return BadRequest("Invalid ObjectId format.");
            }
            var oldChatEntity = await _chatService.GetByIdAsync(objectId);
            if (oldChatEntity is null)
            {
                return NotFound();
            }
            var updateChatEntity = _mapper.Map<ChatEntity>(updateChatDTO);
            updateChatEntity.Id = oldChatEntity.Id;
            await _chatService.UpdateAsync(objectId, updateChatEntity);
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("Invalid ObjectId format.");
            }

            var chat = await _chatService.GetByIdAsync(objectId);

            if (chat is null)
            {
                return NotFound();
            }

            await _chatService.DeleteAsync(objectId);

            return NoContent();
        }
    }
}
