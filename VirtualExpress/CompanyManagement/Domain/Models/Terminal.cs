
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.CompanyManagement.Domain.Models
{
    public class Terminal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public IList<Dispatcher> Dispatchers { get; set; } = new List<Dispatcher>();
    }
}
