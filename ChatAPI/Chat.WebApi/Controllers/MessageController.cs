using Chat.Application.DTOs.Chat;
using Chat.Application.DTOs.Message;
using Chat.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebApi.Controllers
{
    [Route("api/chats/{chatId:Guid}/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(Guid chatId)
        {
            var gotMessageDTO = await _messageService.GetAllAsync(chatId);
            return Ok(gotMessageDTO);
        }

        [ActionName("GetByIdAsync")]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid chatId, Guid id)
        {
            var gotMessageDTO = await _messageService.GetByIdAsync(chatId,id);
            return Ok(gotMessageDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Guid chatId,MessageDTO newMessageDTO)
        {
            var chatEntity = await _messageService.CreateAsync(chatId, newMessageDTO);
            newMessageDTO = _messageService.MessageDTO;
            return CreatedAtAction(nameof(GetByIdAsync), new { id = newMessageDTO.Id }, newMessageDTO);
        }


        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid chatId, Guid id, [FromBody] MessageDTO updateMessageDTO)
        {
            await _messageService.UpdateAsync(chatId,updateMessageDTO, id);
            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid chatId,Guid id)
        {
            await _messageService.DeleteAsync(chatId,id);
            return NoContent();
        }

    }
}
