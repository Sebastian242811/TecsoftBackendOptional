using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;

namespace VirtualExpress.Communication.Domain.Models
{
    public class CustomerServiceEmployee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TerminalId { get; set; }

        public Terminal Terminal { get; set; }

        public IList<Message> Messages { get; set; } = new List<Message>();
    }
}
