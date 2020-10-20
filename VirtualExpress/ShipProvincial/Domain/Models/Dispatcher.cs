using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.ShipProvincial.Domain.Models
{
    public class Dispatcher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DNI { get; set; }
        public IList<Package> Packages { get; set; }
    }
}
