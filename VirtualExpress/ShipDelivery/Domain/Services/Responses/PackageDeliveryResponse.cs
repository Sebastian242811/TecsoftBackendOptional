using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.ShipDelivery.Domain.Models;

namespace VirtualExpress.ShipDelivery.Domain.Services.Responses
{
    public class PackageDeliveryResponse : BaseResponse<PackageDelivery>
    {
        public PackageDeliveryResponse(PackageDelivery resource) : base(resource)
        {
        }

        public PackageDeliveryResponse(string message) : base(message)
        {
        }
    }
}
