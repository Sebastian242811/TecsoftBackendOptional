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
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of Companies", typeof(IEnumerable<CompanyResource>))]
        [ProducesResponseType(typeof(IEnumerable<CompanyResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<CompanyResource>> getAllCompanies()
        {
            var companies = await _companyService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyResource>>(companies);
            return resources;
        }

        [SwaggerResponse(200, "Get company by Id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> getCompanyById(int id)
        {
            var result = await _companyService.FindCompanyById(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var companyResource = _mapper.Map<Company, CompanyResource>(result.Resource);
            return Ok(companyResource);
        }

        [SwaggerResponse(200, "Save company", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> createCompany([FromBody] SaveCompanyResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var company = _mapper.Map<SaveCompanyResource, Company>(resource);
            var result = await _companyService.SaveAsync(company);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var companyResource = _mapper.Map<Company, CompanyResource>(result.Resource);
            return Ok(companyResource);
        }

        [SwaggerResponse(200, "Update company", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> updateCompany(int id, [FromBody] SaveCompanyResource resource)
        {
            var company = _mapper.Map<SaveCompanyResource, Company>(resource);
            var result = await _companyService.UpdateAsync(id, company);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var companyResource = _mapper.Map<Company, CompanyResource>(result.Resource);
            return Ok(companyResource);
        }

        [SwaggerResponse(200, "Delete company", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCompany(int id)
        {
            var result = await _companyService.DeleteAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var companyResource = _mapper.Map<Company, CompanyResource>(result.Resource);
            return Ok(companyResource);
        }
    }
}
