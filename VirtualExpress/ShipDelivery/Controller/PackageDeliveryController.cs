using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualExpress.General.Extensions;
using VirtualExpress.ShipDelivery.Domain.Models;
using VirtualExpress.ShipDelivery.Domain.Services;
using VirtualExpress.ShipDelivery.Resources;

namespace VirtualExpress.ShipDelivery.Controller
{
    [Route("api/packagedeliveries")]
    [ApiController]
    public class PackageDeliveryController : ControllerBase
    {
        private readonly IPackageDeliveryService _packageDeliveryService;
        private readonly IMapper _mapper;

        public PackageDeliveryController(IMapper mapper, IPackageDeliveryService packageDeliveryService)
        {
            _mapper = mapper;
            _packageDeliveryService = packageDeliveryService;
        }

        [SwaggerResponse(200, "List of Package for a delivery", typeof(IEnumerable<PackageDeliveryResource>))]
        [ProducesResponseType(typeof(IEnumerable<PackageDeliveryResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<PackageDeliveryResource>> GetAllAsync()
        {
            var packagesdeliveries = await _packageDeliveryService.ListAsync();
            var resource = _mapper.Map<IEnumerable<PackageDelivery>, IEnumerable<PackageDeliveryResource>>(packagesdeliveries);

            return resource;
        }

        [SwaggerResponse(200, "Save packages into a delivery", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SavePackageDeliveryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var city = _mapper.Map<SavePackageDeliveryResource, PackageDelivery>(resource);
            // TODO: Implement Response Logic
            var result = await _packageDeliveryService.SaveAsync(city);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var packResource = _mapper.Map<PackageDelivery, PackageDeliveryResource>(result.Resource);

            return Ok(packResource);
        }

        [SwaggerResponse(200, "Edit packages into the deliveries", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SavePackageDeliveryResource resource)
        {
            var packagedel = _mapper.Map<SavePackageDeliveryResource, PackageDelivery>(resource);
            var result = await _packageDeliveryService.UpdateAsync(id, packagedel);

            if (result == null)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<PackageDelivery, PackageDeliveryResource>(result.Resource);
            return Ok(categoryResource);
        }

        [SwaggerResponse(200, "Delete a package froma delivery", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _packageDeliveryService.DeleteAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            return Ok("Delete");
        }
    }
}
