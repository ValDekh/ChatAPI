using Chat.Infrastructure.DataAccess.Contexts;
using Chat.Infrastructure.Repositories;
using Chat.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly Repository<ChatEntity, ChatDbSettings> _chatService;
        private readonly ChatDbSettings _chatDbSettings;
        public ChatController(Repository<ChatEntity, ChatDbSettings> chatService)
        {
            _chatDbSettings.CollectionName = "chatCollection";
            _chatService = chatService;
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
        public async Task<IActionResult> Create(ChatEntity newChat)
        {
            await _chatService.AddAsync(newChat);

            return CreatedAtAction(nameof(Get), new { id = newChat.Id }, newChat);
        }

        
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(ObjectId chatId, ChatEntity updateChatEntity)
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
