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
using Chat.Infrastructure.Factory;
using Chat.Infrastructure.Services;
using Chat.Domain.Context;

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
            var getAllService = new ChatServices<ChatEntity,ChatDTO>(_mapper, _chatService);
            var gotChatDTO = await getAllService.GetAllAsync();
            return Ok(gotChatDTO);
        }

        
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var getByIdService = new ChatServices<ChatEntity, ChatDTO>(_mapper, _chatService);
            var gotChatDTO = await getByIdService.GetByIdAsync(id);
           return Ok(gotChatDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync (ChatDTO newChatDTO)
        {
            var createService = new ChatServices<ChatEntity, ChatDTO>(_mapper, _chatService);
            var chatEntity = await createService.CreateAsync(newChatDTO);
            newChatDTO = createService._TDTO;
           return CreatedAtAction(nameof(Get), new { id = newChatDTO.Id }, newChatDTO);
        }


        [HttpPut("{chatId:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]Guid chatId,[FromBody] ChatDTO updateChatDTO)
        {
            var updateService = new ChatServices<ChatEntity, ChatDTO>(_mapper, _chatService);
            await updateService.UpdateAsync(updateChatDTO, chatId);
            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deleteService = new ChatServices<ChatEntity, ChatDTO>(_mapper, _chatService);
            await deleteService.DeleteAsync(id);
            return NoContent();
        }
    }
}
