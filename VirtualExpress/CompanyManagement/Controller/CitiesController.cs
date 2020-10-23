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

namespace VirtualExpress.CompanyManagement.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CitiesController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of cities registered", typeof(IEnumerable<CityResource>))]
        [ProducesResponseType(typeof(IEnumerable<CityResource>), 200)]
        [HttpGet("city")]
        public async Task<IEnumerable<CityResource>> GetAllCitiesAsync()
        {
            var cities = await _cityService.ListAsync();
            var resources = _mapper.Map<IEnumerable<City>, IEnumerable<CityResource>>(cities);
            return resources;
        }
    }
}
