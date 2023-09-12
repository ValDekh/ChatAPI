using Chat.Application.DTOs.Chat;
using Chat.Application.DTOs.Message;
using Chat.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebApi.Controllers
{
    [Route("api/chats/{chatId:Guid}/users/{userId:Guid}/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(Guid chatId, Guid userId)
        {
            var gotMessageDTO = await _messageService.GetAllAsync(chatId, userId);
            return Ok(gotMessageDTO);
        }

        [ActionName("GetByIdAsync")]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid chatId, Guid userId, Guid id)
        {
            var gotMessageDTO = await _messageService.GetByIdAsync(chatId, userId, id);
            return Ok(gotMessageDTO);
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetMessagesWithPaginationAsync(Guid chatId, Guid userId, int page = 1)
        {
            var paginatedMessages = await _messageService.GetMessagesWithPaginationAsync(chatId, userId, page);
            return Ok(paginatedMessages);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Guid chatId, Guid userId, MessageDTORequest newMessageDTORequest)
        {
            var chatEntity = await _messageService.CreateAsync(chatId, userId, newMessageDTORequest);
            var newMessageDTOResponse = _messageService.MessageDTO;
            return CreatedAtAction(nameof(GetByIdAsync), new { chatId, userId, id = newMessageDTOResponse.Id }, newMessageDTOResponse);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid chatId, Guid userId, Guid id, [FromBody] MessageDTORequest updateMessageDTO)
        {
            await _messageService.UpdateAsync(chatId, userId, updateMessageDTO, id);
            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid chatId, Guid userId, Guid id)
        {
            await _messageService.DeleteAsync(chatId, userId, id);
            return NoContent();
        }
    }
}
