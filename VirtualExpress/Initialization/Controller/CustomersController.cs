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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        //Task<IEnumerable<Customer>> ListAsync();
        [SwaggerResponse(200,"List of Customers",typeof(IEnumerable<CustomerResource>))]
        [ProducesResponseType(typeof(IEnumerable<CustomerResource>),200)]
        [HttpGet("asd")]
        public async Task<IEnumerable<CustomerResource>> getAllCustomers()
        {
            var customers = await _customerService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResource>>(customers);
            return resources;
        }

        //Task<CustomerResponse> FindCustomerById(int customerId);
        [HttpGet("id")]
        public async Task<IActionResult> getCustomerByID(int id)
        {
            var result = await _customerService.FindCustomerById(id);
            
            if (!result.Sucess)
                return BadRequest(result.Message);

            var customer = _mapper.Map<Customer, CustomerResource>(result.Resource);

            return Ok(customer);
        }

        //Task<CustomerResponse> SaveAsync(Customer customer);
        [HttpPost("customers")]
        public async Task<IActionResult> createCustomer([FromBody] SaveCustomerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var customer = _mapper.Map<SaveCustomerResource, Customer>(resource);
            var result = await _customerService.SaveAsync(customer);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var customerResource = _mapper.Map<Customer, CustomerResource>(result.Resource);

            return Ok(customerResource);
        }

        //Task<CustomerResponse> UpdateAsync(int customerId, Customer customer);
        [HttpPut("customers/{Id}")]
        public async Task<IActionResult> updateCustomer(int Id, [FromBody] SaveCustomerResource resource)
        {
            var customer = _mapper.Map<SaveCustomerResource, Customer>(resource);
            var result = await _customerService.UpdateAsync(Id, customer);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var customerResource = _mapper.Map<Customer, CustomerResource>(result.Resource);

            return Ok(customerResource);
        }

        //Task<CustomerResponse> DeleteAsync(int customerId);

    }
}
