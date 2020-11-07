using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.CompanyManagement.Domain.Models
{
    public class ShipTerminal
    {
        public int Id { get; set; }
        public int TerminalOriginId { get; set; }
        public Terminal TerminalOrigin { get; set; }
        public int TerminalDestinyId { get; set; }
        public Terminal TerminalDestiny { get; set; }
        public double Price { get; set; }
        public IList<Package> Packages { get; set; } = new List<Package>();
    }
}
