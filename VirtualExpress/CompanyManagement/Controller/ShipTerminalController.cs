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
    [ApiController]
    [Produces("application/json")]
    [Route("api/shipTerminals")]
    public class ShipTerminalController : ControllerBase
    {
        private readonly IShipTerminalService _shipTerminalService;
        private readonly IMapper _mapper;

        public ShipTerminalController(IShipTerminalService shipTerminalService, IMapper mapper)
        {
            _shipTerminalService = shipTerminalService;
            _mapper = mapper;
        }


        [SwaggerResponse(200, "List of the ShipTerminals", typeof(IEnumerable<ShipTerminalResource>))]
        [ProducesResponseType(typeof(IEnumerable<ShipTerminalResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<ShipTerminalResource>> GetAllAsync()
        {
            var terminals = await _shipTerminalService.ListAsync();
            var resource = _mapper.Map<IEnumerable<ShipTerminal>, IEnumerable<ShipTerminalResource>>(terminals);
            return resource;
        }
        //Task<IEnumerable<ShipTerminal>> ListAsync();
        //Task<ShipTerminalResponse> GetByOriginIdAndDestinyId(int originId, int destinyId);
        [HttpGet("originId/destinyId")]
        public async Task<IActionResult> GetShipTerminalByOriginIdAndDestinyId(int originId, int destinyId)
        {
            var result = await _shipTerminalService.GetByOriginIdAndDestinyId(originId, destinyId);
            var shipTerminalResource = _mapper.Map<ShipTerminal, ShipTerminalResource>(result.Resource);
            return Ok(shipTerminalResource);
        }
        //Task<ShipTerminalResponse> SaveAsync(ShipTerminal shipTerminal);
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveShipTerminalResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var terminal = _mapper.Map<SaveShipTerminalResource, ShipTerminal>(resource);
            var result = await _shipTerminalService.SaveAsync(terminal);

            if (!result.Sucess)
                return BadRequest(result.Message);
            var shipTerminalResource = _mapper.Map<ShipTerminal, ShipTerminalResource>(result.Resource);
            return Ok(shipTerminalResource);
        }
        //Task<ShipTerminalResponse> UpdateAsync(int originid, int destinyId, ShipTerminal shipTerminal);
        [HttpPut("originId/destinyId")]
        public async Task<IActionResult> PutAsync(int originId, int destinyId, [FromBody] SaveShipTerminalResource resource)
        {
            var terminal = _mapper.Map<SaveShipTerminalResource, ShipTerminal>(resource);
            var result = await _shipTerminalService.UpdateAsync(originId, destinyId, terminal);

            if (!result.Sucess)
                return BadRequest(result.Message);
            var shipTerminalResource = _mapper.Map<ShipTerminal, ShipTerminalResource>(result.Resource);
            return Ok(shipTerminalResource);
        }
        //Task<ShipTerminalResponse> DeleteAsync(int originId, int destinyId);
        [HttpDelete("originId/destinyId")]
        public async Task<IActionResult> DeleteAsync(int originId, int destinyId)
        {
            var result = await _shipTerminalService.DeleteAsync(originId, destinyId);

            if (!result.Sucess)
                return BadRequest(result.Message);
            var shipTerminalResource = _mapper.Map<ShipTerminal, ShipTerminalResource>(result.Resource);
            return Ok(shipTerminalResource);
        }

        [SwaggerResponse(200, "List ShipTerminal by OriginId", typeof(IEnumerable<ShipTerminalResource>))]
        [ProducesResponseType(typeof(IEnumerable<ShipTerminalResource>), 200)]
        [HttpGet("id")]
        public async Task<IEnumerable<ShipTerminalResource>> GetllShipTerminalByOriginId(int originId)
        {
            var terminals = await _shipTerminalService.GetShipTerminalsByOriginId(originId);
            var terminalResources = _mapper.Map<IEnumerable<ShipTerminal>, IEnumerable<ShipTerminalResource>>(terminals);
            return terminalResources;
        }

        [SwaggerResponse(200, "List ShipTerminal by OriginId", typeof(IEnumerable<ShipTerminalResource>))]
        [ProducesResponseType(typeof(IEnumerable<ShipTerminalResource>), 200)]
        [HttpGet("companies/id")]
        public async Task<IEnumerable<ShipTerminalResource>> GetAllShipTerminalByCompanyId(int company)
        {
            var terminals = await _shipTerminalService.GetShipTerminalsByCompanyId(company);
            var terminalResources = _mapper.Map<IEnumerable<ShipTerminal>, IEnumerable<ShipTerminalResource>>(terminals);
            return terminalResources;
        }

        [SwaggerResponse(200, "List ShipTerminal by OriginId", typeof(IEnumerable<ShipTerminalResource>))]
        [ProducesResponseType(typeof(IEnumerable<ShipTerminalResource>), 200)]
        [HttpGet("companies/id/city")]
        public async Task<IEnumerable<ShipTerminalResource>> GetCityOriginAndCityDestinyByCompanyId(int company)
        {
            var terminals = await _shipTerminalService.GetCityOriginAndCityDestinyByCompanyId(company);
            var terminalResources = _mapper.Map<IEnumerable<ShipTerminal>, IEnumerable<ShipTerminalResource>>(terminals);
            return terminalResources;
        }
    }
}
