using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.Communication.Domain.Services;
using VirtualExpress.Communication.Resources;
using VirtualExpress.General.Extensions;

namespace VirtualExpress.Communication.Controller
{
    [Route("api/chats")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IMapper _mapper;

        public ChatController(IChatService chatService, IMapper mapper)
        {
            _chatService = chatService;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of Chat", typeof(IEnumerable<ChatResource>))]
        [ProducesResponseType(typeof(IEnumerable<ChatResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<ChatResource>> GetAllAsync()
        {
            var chats = await _chatService.ListAsync();
            var resource = _mapper.Map<IEnumerable<Chat>, IEnumerable<ChatResource>>(chats);
            return resource;
        }

        [SwaggerResponse(200, "List of Company Chat", typeof(IEnumerable<ChatResource>))]
        [ProducesResponseType(typeof(IEnumerable<ChatResource>), 200)]
        [HttpGet("company/{companyId}")]
        public async Task<IEnumerable<ChatResource>> GetAllAsyncByCompanyId(int companyId)
        {
            var chats = await _chatService.ListAsyncByCompanyId(companyId);
            var resource = _mapper.Map<IEnumerable<Chat>, IEnumerable<ChatResource>>(chats);
            return resource;
        }


        [SwaggerResponse(200, "List of Customer Chat", typeof(IEnumerable<ChatResource>))]
        [ProducesResponseType(typeof(IEnumerable<ChatResource>), 200)]
        [HttpGet("customer/{customerId}")]
        public async Task<IEnumerable<ChatResource>> GetAllAsyncByCustomerId(int customerId)
        {
            var chats = await _chatService.ListAsyncByCustomerId(customerId);
            var resource = _mapper.Map<IEnumerable<Chat>, IEnumerable<ChatResource>>(chats);
            return resource;
        }


        [SwaggerResponse(200, "Save chats by entering the name", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveChatResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var chat = _mapper.Map<SaveChatResource, Chat>(resource);
            // TODO: Implement Response Logic
            var result = await _chatService.SaveAsync(chat);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var chatResource = _mapper.Map<Chat, ChatResource>(result.Resource);

            return Ok(chatResource);
        }

        [SwaggerResponse(200, "Edit chats by entering the Id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveChatResource resource)
        {
            var chats = _mapper.Map<SaveChatResource, Chat>(resource);
            var result = await _chatService.UpdateAsync(id, chats);

            if (result == null)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Chat, ChatResource>(result.Resource);
            return Ok(categoryResource);
        }

        [SwaggerResponse(200, "Delete chats by entering the id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _chatService.DeleteAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            return Ok("Delete");
        }
    }
}
