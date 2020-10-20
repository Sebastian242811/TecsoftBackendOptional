using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.ShipDelivery.Domain.Models
{
    public class PackageDelivery
    {
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
    }
}
