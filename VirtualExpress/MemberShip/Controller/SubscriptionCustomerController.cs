using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualExpress.General.Extensions;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.MemberShip.Domain.Services;
using VirtualExpress.MemberShip.Resource;

namespace VirtualExpress.MemberShip.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionCustomerController : ControllerBase
    {
        private readonly ISubscriptionCustomerService _subscriptionCustomerService;
        private readonly IMapper _mapper;

        public SubscriptionCustomerController(ISubscriptionCustomerService subscriptionCustomerService, IMapper mapper)
        {
            _subscriptionCustomerService = subscriptionCustomerService;
            _mapper = mapper;
        }

        [SwaggerResponse(200,"List of Customer Subscription",typeof(IEnumerable<SubscriptionCustomerResource>))]
        [ProducesResponseType(typeof(IEnumerable<SubscriptionCustomerResource>),200)]
        [HttpGet]
        public async Task<IEnumerable<SubscriptionCustomerResource>> getAllSubscriptionCustomer()
        {
            var subscriptionCustomers = await _subscriptionCustomerService.ListAsync();
            var resource = _mapper.Map<IEnumerable<SubscriptionCustomer>, IEnumerable<SubscriptionCustomerResource>>(subscriptionCustomers);
            return resource;
        }

        [SwaggerResponse(200, "Get Customer Subscription by Id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> getSubscriptionCustomerById(int id)
        {
            var result = await _subscriptionCustomerService.FindById(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var subscriptionCustomerResource = _mapper.Map<SubscriptionCustomer, SubscriptionCustomerResource>(result.Resource);
            return Ok(subscriptionCustomerResource);
        }

        [SwaggerResponse(200, "Save Customer Subscription", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> createSubscriptionCustomer([FromBody] SaveSubscriptionCustomerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var subscriptionCustomer = _mapper.Map<SaveSubscriptionCustomerResource, SubscriptionCustomer>(resource);
            var result = await _subscriptionCustomerService.SaveAsync(subscriptionCustomer);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var subscriptionCustomerResource = _mapper.Map<SubscriptionCustomer, SubscriptionCustomerResource>(result.Resource);
            return Ok(subscriptionCustomerResource);
        }

        [SwaggerResponse(200, "Update Customer Subscription", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> updateSubscriptionCustomer(int id, [FromBody] SaveSubscriptionCustomerResource resource)
        {
            var subscriptionCustomer = _mapper.Map<SaveSubscriptionCustomerResource, SubscriptionCustomer>(resource);
            var result = await _subscriptionCustomerService.UpdateAsync(id, subscriptionCustomer);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var subscriptionCustomerResource = _mapper.Map<SubscriptionCustomer, SubscriptionCustomerResource>(result.Resource);
            return Ok(subscriptionCustomer);
        }

        [SwaggerResponse(200, "Delete Customer Subscription", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteSubscriptionCustomer(int id)
        {
            var result = await _subscriptionCustomerService.RemoveAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var subscriptionCustomerResource = _mapper.Map<SubscriptionCustomer, SubscriptionCustomerResource>(result.Resource);
            return Ok(subscriptionCustomerResource);
        }
    }
}
