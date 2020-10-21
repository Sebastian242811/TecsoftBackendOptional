using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.Social.Domain.Models;

namespace VirtualExpress.Social.Domain.Services.Responses
{
    public class CommentaryResponse : BaseResponse<Commentary>
    {
        public CommentaryResponse(Commentary resource) : base(resource)
        {
        }

        public CommentaryResponse(string message) : base(message)
        {
        }
    }
}
