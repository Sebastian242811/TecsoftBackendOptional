using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;

namespace VirtualExpress.Initialization.Domain.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Ruc { get; set; }
        public IList<Terminal> Terminals { get; set; } = new List<Terminal>();
        //Subscription
    }
}
