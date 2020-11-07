using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Services;
using VirtualExpress.CompanyManagement.Resources;
using VirtualExpress.General.Extensions;

namespace VirtualExpress.CompanyManagement.Controller
{
    [Route("api/shipterminals")]
    [ApiController]
    public class ShipTerminalController : ControllerBase
    {
        private readonly IShipTerminalService _shipTerminalService;
        private readonly IMapper _mapper;

        public ShipTerminalController(IShipTerminalService ShipTerminalService, IMapper mapper)
        {
            _shipTerminalService = ShipTerminalService;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of ShipTerminal", typeof(IEnumerable<ShipTerminalResource>))]
        [ProducesResponseType(typeof(IEnumerable<ShipTerminalResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<ShipTerminalResource>> GetAllAsync()
        {
            var shipterminals = await _shipTerminalService.ListAsync();
            var resource = _mapper.Map<IEnumerable<ShipTerminal>, IEnumerable<ShipTerminalResource>>(shipterminals);

            return resource;
        }

        [SwaggerResponse(200, "Save sipterminal by entering the name", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveShipTerminalResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var ShipTerminal = _mapper.Map<SaveShipTerminalResource, ShipTerminal>(resource);
            // TODO: Implement Response Logic
            var result = await _shipTerminalService.SaveAsync(ShipTerminal);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var ShipTerminalResource = _mapper.Map<ShipTerminal, ShipTerminalResource>(result.Resource);

            return Ok(ShipTerminalResource);
        }

        [SwaggerResponse(200, "Edit shipterminals by entering the Id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveShipTerminalResource resource)
        {
            var ShipTerminal = _mapper.Map<SaveShipTerminalResource, ShipTerminal>(resource);
            var result = await _shipTerminalService.UpdateAsync(id, ShipTerminal);

            if (result == null)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<ShipTerminal, ShipTerminalResource>(result.Resource);
            return Ok(categoryResource);
        }

        [SwaggerResponse(200, "Delete shipterminals by entering the id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _shipTerminalService.DeleteAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            return Ok("Delete");
        }
    }
}
