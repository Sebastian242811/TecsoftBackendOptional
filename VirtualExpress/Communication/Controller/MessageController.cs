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
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of Message", typeof(IEnumerable<MessageResource>))]
        [ProducesResponseType(typeof(IEnumerable<MessageResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<MessageResource>> GetAllAsync()
        {
            var messages = await _messageService.ListAsync();
            var resource = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(messages);

            return resource;
        }

        [SwaggerResponse(200, "Save messages by entering the name", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveMessageResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var message = _mapper.Map<SaveMessageResource, Message>(resource);
            // TODO: Implement Response Logic
            var result = await _messageService.SaveAsync(message);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var chatResource = _mapper.Map<Message, MessageResource>(result.Resource);

            return Ok(chatResource);
        }

        [SwaggerResponse(200, "Edit messages by entering the Id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveMessageResource resource)
        {
            var messages = _mapper.Map<SaveMessageResource, Message>(resource);
            var result = await _messageService.UpdateAsync(id, messages);

            if (result == null)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Message, MessageResource>(result.Resource);
            return Ok(categoryResource);
        }

        [SwaggerResponse(200, "Delete messages by entering the id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _messageService.DeleteAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            return Ok("Delete");
        }
    }
}
