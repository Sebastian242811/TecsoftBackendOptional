using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.ShipProvincial.Domain.Services.Responses
{
    public class ChangeStateResponse : BaseResponse<ChangeState>
    {
        public ChangeStateResponse(ChangeState resource) : base(resource)
        {
        }

        public ChangeStateResponse(string message) : base(message)
        {
        }
    }
}
