using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;

namespace VirtualExpress.Communication.Domain.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public DateTime PostDate { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public IList<Message> Messages { get; set; } = new List<Message>();
    }
}
