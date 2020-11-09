using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualExpress.General.Extensions;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.ShipProvincial.Domain.Services;
using VirtualExpress.ShipProvincial.Resources;

namespace VirtualExpress.ShipProvincial.Controller
{
    [Route("api/changestates")]
    [ApiController]
    public class ChangeStateController : ControllerBase
    {
        private readonly IChangeStateService _ChangeStateService;
        private readonly IMapper _mapper;

        public ChangeStateController(IChangeStateService ChangeStateService, IMapper mapper)
        {
            _ChangeStateService = ChangeStateService;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of Changes in te package state", typeof(IEnumerable<ChangeStateResource>))]
        [ProducesResponseType(typeof(IEnumerable<ChangeStateResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<ChangeStateResource>> GetAllAsync()
        {
            var cities = await _ChangeStateService.ListAsync();
            var resource = _mapper.Map<IEnumerable<ChangeState>, IEnumerable<ChangeStateResource>>(cities);

            return resource;
        }

        [SwaggerResponse(200, "List of Changes in te package state", typeof(IEnumerable<ChangeStateResource>))]
        [ProducesResponseType(typeof(IEnumerable<ChangeStateResource>), 200)]
        [HttpGet("package/{id}")]
        public async Task<IEnumerable<ChangeStateResource>> GetAllAsyncbypackageid(int id)
        {
            var cities = await _ChangeStateService.ListAsyncbypackageid(id);
            var resource = _mapper.Map<IEnumerable<ChangeState>, IEnumerable<ChangeStateResource>>(cities);

            return resource;
        }

        [SwaggerResponse(200, "Save changes in the pakage state", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveChangeStateResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var ChangeState = _mapper.Map<SaveChangeStateResource, ChangeState>(resource);
            // TODO: Implement Response Logic
            var result = await _ChangeStateService.SaveAsync(ChangeState);


            if (!result.Sucess)
                return BadRequest(result.Message);

            var ChangeStateResource = _mapper.Map<ChangeState, ChangeStateResource>(result.Resource);

            return Ok(ChangeStateResource);
        }
    }
}
