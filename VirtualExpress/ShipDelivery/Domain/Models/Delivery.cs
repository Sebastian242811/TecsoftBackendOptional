using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.ShipDelivery.Domain.Models;

namespace VirtualExpress.Initialization.Domain.Model
{
    public class Delivery
    {
        public int Id { get; set; }
        public string Arrival { get; set; }
        public double Price { get; set; }
        public IList<PackageDelivery> PackageDeliveries { get; set; } = new List<PackageDelivery>();
    }
}
