using Chat.Application.DTOs.Chat;
using Chat.Application.DTOs.Message;
using Chat.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var gotMessageDTO = await _messageService.GetAllAsync();
            return Ok(gotMessageDTO);
        }

        [ActionName("GetByIdAsync")]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var gotMessageDTO = await _messageService.GetByIdAsync(id);
            return Ok(gotMessageDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MessageDTO newMessageDTO)
        {
            var chatEntity = await _messageService.CreateAsync(newMessageDTO);
            newMessageDTO = _messageService.MessageDTO;
            return CreatedAtAction(nameof(GetByIdAsync), new { id = newMessageDTO.Id }, newMessageDTO);
        }


        [HttpPut("{chatId:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid chatId, [FromBody] MessageDTO updateMessageDTO)
        {
            await _messageService.UpdateAsync(updateMessageDTO, chatId);
            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _messageService.DeleteAsync(id);
            return NoContent();
        }

    }
}
