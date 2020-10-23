using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.ShipDelivery.Domain.Models;

namespace VirtualExpress.ShipDelivery.Domain.Services.Responses
{
    public class DeliveryResponse : BaseResponse<Delivery>
    {
        public DeliveryResponse(Delivery resource) : base(resource)
        {
        }

        public DeliveryResponse(string message) : base(message)
        {
        }
    }
}
