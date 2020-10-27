using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Services;
using VirtualExpress.CompanyManagement.Resources;
using VirtualExpress.General.Extensions;

namespace VirtualExpress.CompanyManagement.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of City", typeof(IEnumerable<CityResource>))]
        [ProducesResponseType(typeof(IEnumerable<CityResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<CityResource>> GetAllAsync()
        {
            var cities = await _cityService.ListAsync();
            var resource = _mapper.Map<IEnumerable<City>, IEnumerable<CityResource>>(cities);

            return resource;
        }

        [SwaggerResponse(200, "Save cities by entering the name", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCityResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var city = _mapper.Map<SaveCityResource, City>(resource);
            // TODO: Implement Response Logic
            var result = await _cityService.SaveAsync(city);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var cityResource = _mapper.Map<City, CityResource>(result.Resource);

            return Ok(cityResource);
        }

        [SwaggerResponse(200, "Edit cities by entering the Id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCityResource resource)
        {
            var city = _mapper.Map<SaveCityResource, City>(resource);
            var result = await _cityService.UpdateAsync(id, city);

            if (result == null)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<City, CityResource>(result.Resource);
            return Ok(categoryResource);
        }

        [SwaggerResponse(200, "Delete cities by entering the id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cityService.DeleteAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            return Ok("Delete");
        }
    }
}
