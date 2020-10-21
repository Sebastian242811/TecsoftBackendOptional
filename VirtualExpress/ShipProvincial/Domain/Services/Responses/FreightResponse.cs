using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.ShipProvincial.Domain.Services.Responses
{
    public class FreightResponse : BaseResponse<Freight>
    {
        public FreightResponse(Freight resource) : base(resource)
        {
        }

        public FreightResponse(string message) : base(message)
        {
        }
    }
}
