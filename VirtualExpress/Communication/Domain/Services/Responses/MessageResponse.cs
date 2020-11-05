using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.General.Response;

namespace VirtualExpress.Communication.Domain.Services.Responses
{
    public class MessageResponse : BaseResponse<Message>
    {
        public MessageResponse(Message resource) : base(resource)
        {
        }

        public MessageResponse(string message) : base(message)
        {
        }
    }
}
