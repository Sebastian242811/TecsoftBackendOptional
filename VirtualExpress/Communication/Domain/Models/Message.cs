using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;

namespace VirtualExpress.Communication.Domain.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
