using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.ShipDelivery.Domain.Models;

namespace VirtualExpress.ShipProvincial.Domain.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public EPriority Priority { get; set; }
        public EState State { get; set; }
        public string Weight { get; set; }
        public double Discount { get; set; }
        public int? FerightId { get; set; }
        public virtual Freight Freight { get; set; }
        public int? DispatcherId { get; set; }
        public virtual Dispatcher Dispatcher { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public IList<PackageDelivery> PackageDeliveries { get; set; } = new List<PackageDelivery>();
        public IList<ChangeState> ChangesStates { get; set; } = new List<ChangeState>();
        public int TerminalOriginId { get; set; }
        public int TerminalDestinyId { get; set; }
        public ShipTerminal ShipTerminal { get; set; }
    }
}
