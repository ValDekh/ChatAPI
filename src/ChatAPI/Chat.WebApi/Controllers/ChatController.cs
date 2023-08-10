using Chat.Infrastructure.DataAccess.Contexts;
using Chat.Infrastructure.Repositories;
using Chat.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Chat.Domain.Common.Interfaces;
using AutoMapper;
using Chat.Application.DTOs;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;
using System;

namespace Chat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IRepository<ChatEntity> _chatService;
        private readonly IMapper _mapper;

        public ChatController(IRepository<ChatEntity> chatService, IMapper mapper)
        {
            _chatService = chatService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatDTO>>> GetAll()
        {
            var chats = await _chatService.GetAllAsync();
            var getedChatDTO = _mapper.Map<IEnumerable<ChatDTO>>(chats);
            return Ok(chats);
        }


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<ChatDTO>> Get([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("Invalid ObjectId format.");
            }

            var chat = await _chatService.GetByIdAsync(objectId);

            if (chat == null)
            {
                return NotFound();
            }
            var getedChatDTO = _mapper.Map<ChatDTO>(chat);
            return Ok(getedChatDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChatDTO newChatDTO)
        {
            var newChat = _mapper.Map<ChatEntity>(newChatDTO);


            await _chatService.AddAsync(newChat);

            return CreatedAtAction(nameof(Get), new { id = newChat.Id }, newChat);

        }


        [HttpPut("{id:length(24)}")]
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
        public async Task<IActionResult> Delete([FromQuery] string id)
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
