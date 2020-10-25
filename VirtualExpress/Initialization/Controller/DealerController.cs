using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualExpress.General.Extensions;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.Initialization.Domain.Services;
using VirtualExpress.Initialization.Resource;

namespace VirtualExpress.Initialization.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        private readonly IDealerService _dealerService;
        private readonly IMapper _mapper;

        public DealerController(IDealerService dealerService, IMapper mapper)
        {
            _dealerService = dealerService;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of Dealers", typeof(IEnumerable<DealerResource>))]
        [ProducesResponseType(typeof(IEnumerable<DealerResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<DealerResource>> getAllDealers()
        {
            var dealers = await _dealerService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Dealer>, IEnumerable<DealerResource>>(dealers);
            return resources;
        }

        [SwaggerResponse(200, "Dealer by Id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> getDealerById(int id)
        {
            var result = await _dealerService.FindEmployeeById(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var dealerResource = _mapper.Map<Dealer, DealerResource>(result.Resource);
            return Ok(dealerResource);
        }

        [SwaggerResponse(200, "Save Dealer", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> createDealer([FromBody] SaveDealerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var dealer = _mapper.Map<SaveDealerResource, Dealer>(resource);
            var result = await _dealerService.SaveAsync(dealer);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var dealerResource = _mapper.Map<Dealer, DealerResource>(result.Resource);
            return Ok(dealerResource);
        }

        [SwaggerResponse(200, "Update Dealer", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> updateDealer(int id, [FromBody] SaveDealerResource resource)
        {
            var dealer = _mapper.Map<SaveDealerResource, Dealer>(resource);
            var result = await _dealerService.UpdateAsync(id, dealer);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var dealerResource = _mapper.Map<Dealer, DealerResource>(result.Resource);
            return Ok(dealerResource);
        }

        [SwaggerResponse(200, "Delete Dealer", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteDealer(int id)
        {
            var result = await _dealerService.DeleteAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var dealerResource = _mapper.Map<Dealer, DealerResource>(result.Resource);
            return Ok(dealerResource);
        }
    }
}
