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
using Chat.Infrastructure.Factories;
using Chat.Infrastructure.Services;
using Chat.Domain.Context;
using Chat.Application.Services.Interfaces;

namespace Chat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly IChatService _chatServices;

        public ChatController(IChatService chatServices)
        {
            _chatServices = chatServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var gotChatDTO = await _chatServices.GetAllAsync();
            return Ok(gotChatDTO);
        }

        [ActionName("GetByIdAsync")]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var gotChatDTO = await _chatServices.GetByIdAsync(id);
            return Ok(gotChatDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ChatDTORequest newChatDTO)
        {
            var chatEntity = await _chatServices.CreateAsync(newChatDTO);
            var newChatDTOResponse = _chatServices.ChatDTOResponse;
            return CreatedAtAction(nameof(GetByIdAsync), new { id = newChatDTOResponse.Id }, newChatDTOResponse);
        }


        [HttpPut("{chatId:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid chatId, [FromBody] ChatDTORequest updateChatDTO)
        {
            await _chatServices.UpdateAsync(updateChatDTO, chatId);
            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _chatServices.DeleteAsync(id);
            return NoContent();
        }
    }
}
