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
            var getAllService = new GetAllService<ChatEntity, ChatDTO>(_mapper, _chatService);
            var gotChatDTO = await getAllService.GetAllAsync();
            return Ok(gotChatDTO);
        }


        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetByIdAsync(string id)
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
            var createService = new CreateService<ChatEntity,ChatDTO>(_mapper, _chatService, newChatDTO);
            var chatEntity = await createService.CreateAsync();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = chatEntity.Id.ToString() }, chatEntity);
            //if (ObjectId.TryParse((chatEntity.Id).ToString(), out _))
            //{
            //    return CreatedAtAction(nameof(GetByIdAsync), new { id = chatEntity.Id.ToString() }, chatEntity);
            //}
            //else
            //{
            //    return BadRequest("Invalid ObjectId");
            //}
            //return Ok(chatEntity);
        }


        [HttpPut("{chatId:length(24)}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]string chatId,[FromBody] ChatDTO updateChatDTO)
        {
            var updateService = new UpdateService<ChatEntity, ChatDTO>(_mapper, _chatService, updateChatDTO, chatId);
            var isUpdateSucces = await updateService.UpdateAsync();
            //if (!isUpdateSucces)
            //{
            //    return NotFound();
            //}
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var deleteService = new DeleteService<ChatEntity, ChatDTO>(_mapper, _chatService, id);
            var isDeleteSucces = await deleteService.DeleteAsync();
            
            return NoContent();
        }
    }
}
