using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualExpress.General.Extensions;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.ShipProvincial.Domain.Services;
using VirtualExpress.ShipProvincial.Resources;

namespace VirtualExpress.ShipProvincial.Controller
{
    [Route("api/packages")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _PackageService;
        private readonly IMapper _mapper;

        public PackageController(IPackageService PackageService, IMapper mapper)
        {
            _PackageService = PackageService;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of package", typeof(IEnumerable<PackageResource>))]
        [ProducesResponseType(typeof(IEnumerable<PackageResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<PackageResource>> GetAllAsync()
        {
            var packages = await _PackageService.ListAsync();
            var resource = _mapper.Map<IEnumerable<Package>, IEnumerable<PackageResource>>(packages);

            return resource;
        }

        [SwaggerResponse(200, "Save package.    ATTENTION: there are 4 options of state values: 'En_camino' 'En_espera' 'Retrasado' 'En_termina_destino' and 3 options of priority values: 'Alta' 'Media' 'Baja'", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SavePackageResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var package = _mapper.Map<SavePackageResource, Package>(resource);
            var result = await _PackageService.SaveAsync(package);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var packageResource = _mapper.Map<Package, PackageResource>(result.Resource);

            return Ok(packageResource);
        }

        [SwaggerResponse(200, "Update package", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsyng(int id, [FromBody] SavePackageResource resource)
        {
            var package = _mapper.Map<SavePackageResource, Package>(resource);
            var result = await _PackageService.UpdateAsync(id, package);

            if (result == null)
                return BadRequest(result.Message);

            var packageResource = _mapper.Map<Package, PackageResource>(result.Resource);

            return Ok(packageResource);
        }

        [SwaggerResponse(200, "Delete package", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _PackageService.DeleteAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            return Ok("Delete");
        }

        [SwaggerResponse(200, "Update package State", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("updatestate/{id}")]
        public async Task<IActionResult> PutStateAsyng(int id, [FromBody] UpdateStateResource resource)
        {
            var package = _mapper.Map<UpdateStateResource, Package>(resource);
            var result = await _PackageService.UpdateStateAsync(id, package);

            if (result == null)
                return BadRequest(result.Message);

            var packageResource = _mapper.Map<Package, PackageResource>(result.Resource);

            return Ok(packageResource);
        }


        [SwaggerResponse(200, "List of package by state", typeof(IEnumerable<PackageResource>))]
        [ProducesResponseType(typeof(IEnumerable<PackageResource>), 200)]
        [HttpGet("state/")]
        public async Task<IEnumerable<PackageResource>> GetPackagesByState(int state)
        {
            var packages = await _PackageService.ListByState(state);
            var resource = _mapper.Map<IEnumerable<Package>, IEnumerable<PackageResource>>(packages);

            return resource;
        }
    }
}
