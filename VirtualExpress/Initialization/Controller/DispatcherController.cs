using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualExpress.General.Extensions;
using VirtualExpress.Initialization.Domain.Models;
using VirtualExpress.Initialization.Domain.Services;
using VirtualExpress.Initialization.Resources;

namespace VirtualExpress.ShipProvincial.Controller
{
    [Route("api/dispatchers")]
    [ApiController]
    public class DispatcherController : ControllerBase
    {
        private readonly IDispatcherService _dispatcherService;
        private readonly IMapper _mapper;

        public DispatcherController(IMapper mapper, IDispatcherService dispatcherService)
        {
            _mapper = mapper;
            _dispatcherService = dispatcherService;
        }


        [SwaggerResponse(200, "List of Dispatchers", typeof(IEnumerable<DispatcherResource>))]
        [ProducesResponseType(typeof(IEnumerable<DispatcherResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<DispatcherResource>> GetAllAsync()
        {
            var dispatcher = await _dispatcherService.ListAsync();
            var resource = _mapper.Map<IEnumerable<Dispatcher>, IEnumerable<DispatcherResource>>(dispatcher);

            return resource;
        }

        [SwaggerResponse(200, "Get Dispatcher", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dispatcherService.GetById(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var resourceDispatcher = _mapper.Map<Dispatcher,DispatcherResource>(result.Resource);

            return Ok(resourceDispatcher);
        }

        [SwaggerResponse(200, "Save Dispatcher", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveDispatcherResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var dispatcher = _mapper.Map<SaveDispatcherResource, Dispatcher>(resource);
            // TODO: Implement Response Logic
            var result = await _dispatcherService.SaveAsync(dispatcher);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var dispatcherResource = _mapper.Map<Dispatcher, DispatcherResource>(result.Resource);

            return Ok(dispatcherResource);
        }

        [SwaggerResponse(200, "Update Dispatcher", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveDispatcherResource resource)
        {
            var dispatcher = _mapper.Map<SaveDispatcherResource, Dispatcher>(resource);
            var result = await _dispatcherService.UpdateAsync(id, dispatcher);

            if (result == null)
                return BadRequest(result.Message);

            var dispatcherResource = _mapper.Map<Dispatcher, DispatcherResource>(result.Resource);
            return Ok(dispatcherResource);
        }

        [SwaggerResponse(200, "Delete Dispatcher", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _dispatcherService.DeleteAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            return Ok("Delete");
        }
    }
}
