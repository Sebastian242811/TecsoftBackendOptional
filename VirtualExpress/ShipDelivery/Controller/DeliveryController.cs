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
using VirtualExpress.ShipDelivery.Domain.Services;
using VirtualExpress.ShipDelivery.Resources;

namespace VirtualExpress.ShipDelivery.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _DeliveryStateService;
        private readonly IMapper _mapper;

        public DeliveryController(IDeliveryService DeliveryStateService, IMapper mapper)
        {
            _DeliveryStateService = DeliveryStateService;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of Delivery", typeof(IEnumerable<DeliveryResource>))]
        [ProducesResponseType(typeof(IEnumerable<DeliveryResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<DeliveryResource>> GetAllAsync()
        {
            var Deliverys = await _DeliveryStateService.ListAsync();
            var resource = _mapper.Map<IEnumerable<Delivery>, IEnumerable<DeliveryResource>>(Deliverys);

            return resource;
        }

        [SwaggerResponse(200, "Save Delivery", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveDeliveryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var Delivery = _mapper.Map<SaveDeliveryResource, Delivery>(resource);
            var result = await _DeliveryStateService.SaveAsync(Delivery);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var DeliveryResource = _mapper.Map<Delivery, DeliveryResource>(result.Resource);

            return Ok(DeliveryResource);
        }

        [SwaggerResponse(200, "Update Delivery", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("id")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveDeliveryResource resource)
        {
            var Delivery = _mapper.Map<SaveDeliveryResource, Delivery>(resource);
            var result = await _DeliveryStateService.UpdateAsync(id, Delivery);

            if (result == null)
                return BadRequest(result.Message);

            var DeliveryResource = _mapper.Map<Delivery, DeliveryResource>(result.Resource);

            return Ok(DeliveryResource);
        }

        [SwaggerResponse(200, "Delete Delivery", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _DeliveryStateService.DeleteAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            return Ok("Delete");
        }
    }
}
