using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualExpress.ShipDelivery.Domain.Models;
using VirtualExpress.ShipDelivery.Domain.Services;
using VirtualExpress.ShipDelivery.Resources;
using VirtualExpress.ShipProvincial.Resources;

namespace VirtualExpress.ShipDelivery.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
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

        [SwaggerResponse(200, "Home delivery list with their assigned packets", typeof(IEnumerable<PackageDeliveryResource>))]
        [ProducesResponseType(typeof(IEnumerable<PackageDeliveryResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<PackageDeliveryResource>> GetAllAsync()
        {
            var companies = await _packageDeliveryService.ListAsync();
            var resource = _mapper.Map<IEnumerable<PackageDelivery>, IEnumerable<PackageDeliveryResource>>(companies);

            return resource;
        }

    }
}
