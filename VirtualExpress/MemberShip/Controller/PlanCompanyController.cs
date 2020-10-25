using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class PlanCompanyController : ControllerBase
    {
        private readonly IPlanCompanyService _planCompanyService;
        private readonly IMapper _mapper;

        public PlanCompanyController(IPlanCompanyService planCompanyService, IMapper mapper)
        {
            _planCompanyService = planCompanyService;
            _mapper = mapper;
        }

        [SwaggerResponse(200,"List of Plans for Company",typeof(IEnumerable<PlanCompanyResource>))]
        [ProducesResponseType(typeof(IEnumerable<PlanCompanyResource>),200)]
        [HttpGet]
        public async Task<IEnumerable<PlanCompanyResource>> getAllPlanCompanies()
        {
            var planCompanies = await _planCompanyService.ListAsync();
            var resource = _mapper.Map<IEnumerable<PlanCompany>, IEnumerable<PlanCompanyResource>>(planCompanies);
            return resource;
        }

        [SwaggerResponse(200, "Get a Plan for Company by Id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> getPlanCompanyById(int id)
        {
            var result = await _planCompanyService.FindById(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var resource = _mapper.Map<PlanCompany, PlanCompanyResource>(result.Resource);
            return Ok(resource);
        }

        [SwaggerResponse(200, "Save Plan for Company", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> createPlanCompany([FromBody] SavePlanCompanyResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var planCompany = _mapper.Map<SavePlanCompanyResource, PlanCompany>(resource);
            var result = await _planCompanyService.SaveAsync(planCompany);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var planCompanyResource = _mapper.Map<PlanCompany, PlanCompanyResource>(result.Resource);
            
            return Ok(planCompanyResource);
        }

        [SwaggerResponse(200, "Update Plan for Company", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> updatePlanCompany(int id, [FromBody] SavePlanCompanyResource resource)
        {
            var planCompany = _mapper.Map<SavePlanCompanyResource, PlanCompany>(resource);
            var result = await _planCompanyService.UpdateAsync(id, planCompany);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var planCompanyResource = _mapper.Map<PlanCompany, PlanCompanyResource>(result.Resource);
            return Ok(planCompanyResource);
        }

        [SwaggerResponse(200, "Delete Plan for Company", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deletePlanCompany(int id)
        {
            var result = await _planCompanyService.RemoveAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var planCompanyResource = _mapper.Map<PlanCompany, PlanCompanyResource>(result.Resource);
            return Ok(planCompanyResource);
        }
    }
}
