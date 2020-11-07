using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.ShipProvincial.Resources
{
    public class PackageResource
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public string Priority { get; set; }
        public string State { get; set; }
        public string Weight { get; set; }
        public double Discount { get; set; }
        public int FerightId { get; set; }
        public int DispatcherId { get; set; }
        public int CustomerId { get; set; }
        public int ShipTerminalId { get; set; }
    }
}
