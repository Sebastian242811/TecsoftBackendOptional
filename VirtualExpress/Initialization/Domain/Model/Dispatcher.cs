using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.Initialization.Domain.Models
{
    public class Dispatcher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DNI { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int TerminalId { get; set; }
        public Terminal Terminal { get; set; }
        public IList<Package> Packages { get; set; }
    }
}
