using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.CompanyManagement.Resources
{
    public class SaveShipTerminalResource
    {
        public int TerminalOriginId { get; set; }
        public int TerminalDestinyId { get; set; }
        public double Price { get; set; }
    }
}
