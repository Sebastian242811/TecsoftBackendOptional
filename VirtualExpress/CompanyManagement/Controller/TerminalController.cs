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
    [Produces("application/json")]
    [Route("api/terminals")]
    [ApiController]
    public class TerminalController : ControllerBase
    {
        private readonly ITerminalService _terminalService;
        private readonly IMapper _mapper;

        public TerminalController(ITerminalService terminalService, IMapper mapper)
        {
            _terminalService = terminalService;
            _mapper = mapper;
        }


        [SwaggerResponse(200, "List of terminals", typeof(IEnumerable<TerminalResource>))]
        [ProducesResponseType(typeof(IEnumerable<TerminalResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<TerminalResource>> GetAllAsync()
        {
            var terminals = await _terminalService.ListAsync();
            var resource = _mapper.Map<IEnumerable<Terminal>, IEnumerable<TerminalResource>>(terminals);
            return resource;
        }

        [SwaggerResponse(200, "Get terminal by id", typeof(IEnumerable<TerminalResource>))]
        [ProducesResponseType(typeof(IEnumerable<TerminalResource>), 200)]
        [HttpGet("{id}")]
        public async Task<TerminalResource> GetterminalbyId(int id)
        {
            var terminals = await _terminalService.GetByIdAsync(id);
            var terminalsel = terminals.Resource;
            var resource = _mapper.Map<Terminal, TerminalResource>(terminalsel);
            return resource;
        }

        [SwaggerResponse(200, "Save Terminals", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveTerminalResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var terminal = _mapper.Map<SaveTerminalResource, Terminal>(resource);
            var result = await _terminalService.SaveAssync(terminal);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var terminalResource = _mapper.Map<Terminal, TerminalResource>(result.Resource);

            return Ok(terminalResource);
        }

        [SwaggerResponse(200, "Edit Terminals", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTerminalResource resource)
        {
            var terminal = _mapper.Map<SaveTerminalResource, Terminal>(resource);
            var result = await _terminalService.UpdateAssync(id, terminal);

            if (result == null)
                return BadRequest(result.Message);

            var terminalresource = _mapper.Map<Terminal, TerminalResource>(result.Resource);
            return Ok(terminalresource);
        }

        [SwaggerResponse(200, "Delete Terminals", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _terminalService.DeleteAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            return Ok("Delete");
        }
    }
}
