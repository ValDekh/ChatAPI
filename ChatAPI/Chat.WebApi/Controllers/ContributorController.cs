using Chat.Application.DTOs.Contributer;
using Chat.Application.DTOs.Message;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebApi.Controllers
{
    [Route("api/chats/{chatId:Guid}/{ownerId:Guid}/contributors")]
    [ApiController]
    public class ContributorController : ControllerBase
    {
        private readonly IContributorService _contributorService;
        public ContributorController(IContributorService contributorService)
        {
            _contributorService = contributorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(Guid chatId, Guid ownerId)
        {
            var getAllContribDTOs = await _contributorService.GetAllAsync(chatId, ownerId);
            return Ok(getAllContribDTOs);
        }

        [ActionName("GetByUserIdAsync")]
        [HttpGet("{userId:Guid}")]
        public async Task<IActionResult> GetByUserIdAsync(Guid chatId, Guid ownerId, Guid userId)
        {
            var getContributorDTO = await _contributorService.GetByUserIdAsync(chatId, ownerId, userId);
            return Ok(getContributorDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Guid chatId, Guid ownerId, ContributorDTORequest newContributorDTORequest)
        {
            var chatEntity = await _contributorService.AddContributorAsync(chatId, ownerId, newContributorDTORequest);
            var newContributorDTOResponse = _contributorService.ContributorDTOResponse;
            return CreatedAtAction(nameof(GetByUserIdAsync), new { chatId, ownerId, userId = newContributorDTOResponse.UserId }, newContributorDTOResponse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid chatId, Guid ownerId, [FromBody] ContributorDTORequest newContributorDTO)
        {
            await _contributorService.UpdateAsync(chatId, ownerId, newContributorDTO);
            return Ok();
        }

        [HttpDelete("{userId:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid chatId, Guid ownerId, Guid userId)
        {
            await _contributorService.DeleteAsync(chatId, ownerId, userId);
            return NoContent();
        }
    }
}
