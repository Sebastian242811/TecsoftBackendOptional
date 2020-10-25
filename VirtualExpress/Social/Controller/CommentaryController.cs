using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualExpress.General.Extensions;
using VirtualExpress.Social.Domain.Models;
using VirtualExpress.Social.Domain.Services;
using VirtualExpress.Social.Resources;

namespace VirtualExpress.Social.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentaryController : ControllerBase
    {
        private readonly ICommentaryService _commentaryService;
        private readonly IMapper _mapper;

        public CommentaryController(ICommentaryService commentaryService, IMapper mapper)
        {
            _commentaryService = commentaryService;
            _mapper = mapper;
        }

        [SwaggerResponse(200, "List of commentary", typeof(IEnumerable<CommentaryResource>))]
        [ProducesResponseType(typeof(IEnumerable<CommentaryResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<CommentaryResource>> GetAsync()
        {
            var commentaries = await _commentaryService.ListAsync();
            var resource = _mapper.Map<IEnumerable<Commentary>, IEnumerable<CommentaryResource>>(commentaries);

            return resource;
        }

        [SwaggerResponse(200, "Save Comentary", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCommentaryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var commentary = _mapper.Map<SaveCommentaryResource, Commentary>(resource);
            var result = await _commentaryService.SaveAsync(commentary);

            if (!result.Sucess)
                return BadRequest(result.Message);

            var commentaryResource = _mapper.Map<Commentary, CommentaryResource>(result.Resource);
            return Ok(commentaryResource);
        }

        [SwaggerResponse(200, "Update Comentary", typeof(IActionResult))]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCommentaryResource resource)
        {
            var commentary = _mapper.Map<SaveCommentaryResource, Commentary>(resource);
            var result = await _commentaryService.UpdateAsync(id, commentary);

            if (result == null)
                return BadRequest(result.Message);

            var commentaryResource = _mapper.Map<Commentary, CommentaryResource>(result.Resource);
            return Ok(commentaryResource);
        }
    }
}
