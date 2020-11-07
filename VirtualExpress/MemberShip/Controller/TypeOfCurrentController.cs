using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualExpress.General.Extensions;
using VirtualExpress.MemberShip.Model.Model;
using VirtualExpress.MemberShip.Model.Services;
using VirtualExpress.MemberShip.Resource;

namespace VirtualExpress.MemberShip.Controller
{
    [Route("api/typeofcurrents")]
    [ApiController]
    public class TypeOfCurrentController : ControllerBase
    {
        private readonly ITypeOfCurrentService _typeOfCurrentService;
        private readonly IMapper _mapper;

        public TypeOfCurrentController(ITypeOfCurrentService typeOfCurrentService, IMapper mapper)
        {
            _typeOfCurrentService = typeOfCurrentService;
            _mapper = mapper;
        }

        [SwaggerResponse(200,"List of Type of Current",typeof(IEnumerable<TypeOfCurrentResource>))]
        [ProducesResponseType(typeof(IEnumerable<TypeOfCurrentResource>),200)]
        [HttpGet]
        public async Task<IEnumerable<TypeOfCurrentResource>> getAllTypeOfCurrent()
        {
            var typeOfCurrents = await _typeOfCurrentService.GetAllAsync();
            var resource = _mapper.Map<IEnumerable<TypeOfCurrent>, IEnumerable<TypeOfCurrentResource>>(typeOfCurrents);
            return resource;
        }

        [SwaggerResponse(200, "Get Type of Current by Id", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> getTypeOfCurrentById(int id)
        {
            var result = await _typeOfCurrentService.FindById(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var typeOfCurrentResource = _mapper.Map<TypeOfCurrent, TypeOfCurrentResource>(result.Resource);
            return Ok(typeOfCurrentResource);
        }

        [SwaggerResponse(200, "Save Type of Current", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> createTypeOfCurrent([FromBody] SaveTypeOfCurrentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var typeOfCurrent = _mapper.Map<SaveTypeOfCurrentResource, TypeOfCurrent>(resource);
            var result = await _typeOfCurrentService.SaveAsync(typeOfCurrent);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var typeOfCurrentResource = _mapper.Map<TypeOfCurrent, TypeOfCurrentResource>(result.Resource);
            return Ok(typeOfCurrentResource);
        }

        [SwaggerResponse(200, "Update Type of Current", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> updateTypeOfCurrent(int id, [FromBody] SaveTypeOfCurrentResource resource)
        {
            var typeOfCurrent = _mapper.Map<SaveTypeOfCurrentResource, TypeOfCurrent>(resource);
            var result = await _typeOfCurrentService.UpdateAsync(id, typeOfCurrent);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var typeOfCurrentResource = _mapper.Map<TypeOfCurrent, TypeOfCurrentResource>(result.Resource);
            return Ok(typeOfCurrentResource);
        }

        [SwaggerResponse(200, "Delete Type of Current", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteTypeOfCurrent(int id)
        {
            var result = await _typeOfCurrentService.DeleteAsync(id);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var typeOfCurrentResource = _mapper.Map<TypeOfCurrent, TypeOfCurrentResource>(result.Resource);
            return Ok(typeOfCurrentResource);
        }
    }
}
