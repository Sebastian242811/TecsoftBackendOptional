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
    public class SubscriptionCompanyController : ControllerBase
    {
        private readonly ISubscriptionCompanyService _subscriptionCompany;
        private readonly IMapper _mapper;

        public SubscriptionCompanyController(ISubscriptionCompanyService subscriptionCompany, IMapper mapper)
        {
            _subscriptionCompany = subscriptionCompany;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of Subscription Companies", typeof(IEnumerable<SubscriptionCompanyResource>))]
        [ProducesResponseType(typeof(IEnumerable<SubscriptionCompanyResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<SubscriptionCompanyResource>> getAllPlanCompanies()
        {
            var subscriptionCompanies = await _subscriptionCompany.ListAsync();
            var resource = _mapper.Map<IEnumerable<SubscriptionCompany>, IEnumerable<SubscriptionCompanyResource>>(subscriptionCompanies);
            return resource;
        }

        [SwaggerResponse(200, "Get subscription Company by Id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> getSubscriptionCompanyById(int id)
        {
            var result = await _subscriptionCompany.FindById(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var resource = _mapper.Map<SubscriptionCompany, SubscriptionCompanyResource>(result.Resource);
            return Ok(resource);
        }

        [SwaggerResponse(200, "Save subscription Company", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> createSubscriptionCompany([FromBody] SaveSubscriptionCompanyResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var subscriptionCompany = _mapper.Map<SaveSubscriptionCompanyResource, SubscriptionCompany>(resource);
            var result = await _subscriptionCompany.SaveAsync(subscriptionCompany);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var SubscriptionCompanyResource = _mapper.Map<SubscriptionCompany, SubscriptionCompanyResource>(result.Resource);
            return Ok(SubscriptionCompanyResource);
        }

        [SwaggerResponse(200, "Update subscription Company", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> updateSubscriptionCompany(int id, [FromBody] SaveSubscriptionCompanyResource resource)
        {
            var subscriptionCompany = _mapper.Map<SaveSubscriptionCompanyResource, SubscriptionCompany>(resource);
            var result = await _subscriptionCompany.UpdateAsync(id, subscriptionCompany);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var SubscriptionCompanyResource = _mapper.Map<SubscriptionCompany, SubscriptionCompanyResource>(result.Resource);
            return Ok(SubscriptionCompanyResource);
        }

        [SwaggerResponse(200, "Delete subscription Company", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteSubscriptionCompany(int id)
        {
            var result = await _subscriptionCompany.RemoveAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var SubscriptionCompanyResource = _mapper.Map<SubscriptionCompany, SubscriptionCompanyResource>(result.Resource);
            return Ok(SubscriptionCompanyResource);
        }
    }
}
