using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.ShipProvincial.Domain.Services.Responses
{
    public class DispatcherResponse : BaseResponse<Dispatcher>
    {
        public DispatcherResponse(Dispatcher resource) : base(resource)
        {
        }

        public DispatcherResponse(string message) : base(message)
        {
        }
    }
}
