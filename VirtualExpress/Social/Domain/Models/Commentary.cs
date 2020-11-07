using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;

namespace VirtualExpress.Social.Domain.Models
{
    public class Commentary
    {
        public int Id { get; set; }
        public string Opinion { get; set; }
        public int Valoration { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
