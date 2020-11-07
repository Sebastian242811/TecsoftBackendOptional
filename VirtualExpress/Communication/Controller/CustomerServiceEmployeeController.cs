using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.Communication.Domain.Services;
using VirtualExpress.Communication.Resources;
using VirtualExpress.General.Extensions;

namespace VirtualExpress.Communication.Controller
{
    [Route("api/CustomerServiceEmployees")]
    [ApiController]
    public class CustomerServiceEmployeeController : ControllerBase
    {
        private readonly ICustomerServiceEmployeeService _customerServiceEmployeeService;
        private readonly IMapper _mapper;

        public CustomerServiceEmployeeController(ICustomerServiceEmployeeService customerServiceEmployeeService, IMapper mapper)
        {
            _customerServiceEmployeeService = customerServiceEmployeeService;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of Customer Service Employees", typeof(IEnumerable<CustomerServiceEmployeeResource>))]
        [ProducesResponseType(typeof(IEnumerable<CustomerServiceEmployeeResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<CustomerServiceEmployeeResource>> GetAllAsync()
        {
            var customerServiceEmployees = await _customerServiceEmployeeService.ListAsync();
            var resource = _mapper.Map<IEnumerable<CustomerServiceEmployee>, IEnumerable<CustomerServiceEmployeeResource>>
                (customerServiceEmployees);

            return resource;
        }

        [SwaggerResponse(200, "Save Customer Service Employees by entering the name", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCustomerServiceEmployeeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var customerServiceEmployee = _mapper.Map<SaveCustomerServiceEmployeeResource, CustomerServiceEmployee>(resource);
            // TODO: Implement Response Logic
            var result = await _customerServiceEmployeeService.SaveAsync(customerServiceEmployee);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var customerServiceEmployeeResource = _mapper.Map<CustomerServiceEmployee, CustomerServiceEmployeeResource>(result.Resource);

            return Ok(customerServiceEmployeeResource);
        }

        [SwaggerResponse(200, "Edit Customer Service Employees by entering the Id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCustomerServiceEmployeeResource resource)
        {
            var customerServiceEmployees = _mapper.Map<SaveCustomerServiceEmployeeResource, CustomerServiceEmployee>(resource);
            var result = await _customerServiceEmployeeService.UpdateAsync(id, customerServiceEmployees);

            if (result == null)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<CustomerServiceEmployee, CustomerServiceEmployeeResource>(result.Resource);
            return Ok(categoryResource);
        }

        [SwaggerResponse(200, "Delete Customer Service Employees by entering the id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerServiceEmployeeService.DeleteAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            return Ok("Delete");
        }
    }
}
