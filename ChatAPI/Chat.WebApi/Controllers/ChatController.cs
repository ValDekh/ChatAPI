﻿using Chat.Infrastructure.Repositories;
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
using Chat.Application.Services.Interfaces;

namespace Chat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly IChatServices _chatServices;

        public ChatController(IChatServices chatServices)
        {
            _chatServices = chatServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var gotChatDTO = await _chatServices.GetAllAsync();
            return Ok(gotChatDTO);
        }


        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var gotChatDTO = await _chatServices.GetByIdAsync(id);
            return Ok(gotChatDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ChatDTO newChatDTO)
        {
            var chatEntity = await _chatServices.CreateAsync(newChatDTO);
            newChatDTO = _chatServices.ChatDTO;
            return CreatedAtAction(nameof(Get), new { id = newChatDTO.Id }, newChatDTO);
        }


        [HttpPut("{chatId:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid chatId, [FromBody] ChatDTO updateChatDTO)
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
