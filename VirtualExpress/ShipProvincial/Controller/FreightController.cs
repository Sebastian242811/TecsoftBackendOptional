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
    [Route("api/[controller]")]
    [ApiController]
    public class FreightController : ControllerBase
    {
        private readonly IFreightService _freightService;
        private readonly IMapper _mapper;

        public FreightController(IFreightService freightService, IMapper mapper)
        {
            _freightService = freightService;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of Freight", typeof(IEnumerable<FreightResource>))]
        [ProducesResponseType(typeof(IEnumerable<FreightResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<FreightResource>> GetAllAsync()
        {
            var freights = await _freightService.ListAsync();
            var resource = _mapper.Map<IEnumerable<Freight>, IEnumerable<FreightResource>>(freights);

            return resource;
        }

        [SwaggerResponse(200, "Save Feight", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveFreightResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var freight = _mapper.Map<SaveFreightResource, Freight>(resource);
            var result = await _freightService.SaveAsync(freight);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var freightResource = _mapper.Map<Freight, FreightResource>(result.Resource);
            return Ok(freightResource);
        }

        [SwaggerResponse(200, "Update Freight", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveFreightResource resource)
        {
            var freight = _mapper.Map<SaveFreightResource, Freight>(resource);
            var result = await _freightService.UpdateAsync(id, freight);

            if (result == null)
                return BadRequest(result.Message);

            var freightResource = _mapper.Map<Freight, FreightResource>(result.Resource);
            return Ok(freightResource);
        }

        [SwaggerResponse(200, "Delete Feight", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _freightService.DeleteAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            return Ok("Delete");
        }
    }
}
