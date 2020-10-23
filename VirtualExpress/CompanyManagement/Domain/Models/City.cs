using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;

namespace VirtualExpress.CompanyManagement.Domain.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Terminal> Terminals { get; set; } = new List<Terminal>();
        public IList<Customer> customers { get; set; } = new List<Customer>();
        public IList<Employee> employees { get; set; } = new List<Employee>();
    }
}
