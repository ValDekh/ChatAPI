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
        public async Task<ActionResult<IEnumerable<ChatEntity>>> GetAll()
        {
            var chats = await _chatService.GetAllAsync();
            return Ok(chats);
        }


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<ChatEntity>> Get(ObjectId id)
        {
            var chat = await _chatService.GetByIdAsync(id);

            if (chat == null)
            {
                return NotFound();
            }
            return Ok(chat);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChatDTO newChatDTO)
        {
            var newChat = _mapper.Map<ChatEntity>(newChatDTO);


            await _chatService.AddAsync(newChat);

            return CreatedAtAction(nameof(Get), new { id = newChat.Id }, newChat);

        }


        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update([FromRoute] ObjectId chatId, [FromBody] ChatEntity updateChatEntity)
        {
            var oldChatEntity = await _chatService.GetByIdAsync(chatId);
            if (oldChatEntity is null)
            {
                return NotFound();
            }
            updateChatEntity.Id = oldChatEntity.Id;
            await _chatService.UpdateAsync(chatId, updateChatEntity);
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            var chat = await _chatService.GetByIdAsync(id);

            if (chat is null)
            {
                return NotFound();
            }

            await _chatService.DeleteAsync(id);

            return NoContent();
        }
    }
}
