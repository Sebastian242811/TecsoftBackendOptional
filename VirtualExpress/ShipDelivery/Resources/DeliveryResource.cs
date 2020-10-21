using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.ShipDelivery.Resources
{
    public class DeliveryResource
    {
        public int Id { get; set; }
        public string Arrival { get; set; }
        public double Price { get; set; }
    }
}
