using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;

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
    }
}
