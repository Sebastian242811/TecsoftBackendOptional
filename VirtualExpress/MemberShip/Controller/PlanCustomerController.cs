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
    public class PlanCustomerController : ControllerBase
    {
        private readonly IPlanCustomerService _planCustomerService;
        private readonly IMapper _mapper;

        public PlanCustomerController(IPlanCustomerService planCustomerService, IMapper mapper)
        {
            _planCustomerService = planCustomerService;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of Plans for Customer", typeof(IEnumerable<PlanCustomerResource>))]
        [ProducesResponseType(typeof(IEnumerable<PlanCustomerResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<PlanCustomerResource>> getAllPlanCompanies()
        {
            var planCustomers = await _planCustomerService.ListAsync();
            var resource = _mapper.Map<IEnumerable<PlanCustomer>, IEnumerable<PlanCustomerResource>>(planCustomers);
            return resource;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getPlanCompanyById(int id)
        {
            var result = await _planCustomerService.FindById(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var resource = _mapper.Map<PlanCustomer, PlanCustomerResource>(result.Resource);
            return Ok(resource);
        }

        [HttpPost]
        public async Task<IActionResult> createPlanCompany([FromBody] SavePlanCustomerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var planCustomer = _mapper.Map<SavePlanCustomerResource, PlanCustomer>(resource);
            var result = await _planCustomerService.SaveAsync(planCustomer);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var planCustomerResource = _mapper.Map<PlanCustomer, PlanCustomerResource>(result.Resource);
            return Ok(planCustomerResource);
        }

        [HttpPut]
        public async Task<IActionResult> updatePlanCompany(int id, [FromBody] SavePlanCustomerResource resource)
        {
            var planCustomer = _mapper.Map<SavePlanCustomerResource, PlanCustomer>(resource);
            var result = await _planCustomerService.UpdateAsync(id, planCustomer);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var planCustomerResource = _mapper.Map<PlanCustomer, PlanCustomerResource>(result.Resource);
            return Ok(planCustomerResource);
        }

        [HttpDelete]
        public async Task<IActionResult> deletePlanCompany(int id)
        {
            var result = await _planCustomerService.RemoveAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var planCompanyResource = _mapper.Map<PlanCustomer, PlanCustomerResource>(result.Resource);
            return Ok(planCompanyResource);
        }
    }
}
