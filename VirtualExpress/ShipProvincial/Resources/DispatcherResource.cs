using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.ShipProvincial.Resources
{
    public class DispatcherResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DNI { get; set; }
        public int TerminalId { get; set; }
    }
}
