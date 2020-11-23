using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.CompanyManagement.Resources
{
    public class ShipTerminalResource
    {
        public int Id { get; set; }
        public int TerminalOriginId { get; set; }
        public TerminalResource TerminalOrigin { get; set; }
        public int TerminalDestinyId { get; set; }
        public TerminalResource TerminalDestiny { get; set; }
        public double Price { get; set; }
    }
}
