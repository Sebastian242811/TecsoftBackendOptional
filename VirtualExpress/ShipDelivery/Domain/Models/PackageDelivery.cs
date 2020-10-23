using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.ShipDelivery.Domain.Models
{
    public class PackageDelivery
    {
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        public int PackageId { get; set; }
        public Package Package { get; set; }
    }
}
