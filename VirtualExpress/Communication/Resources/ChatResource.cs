using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Resource;

namespace VirtualExpress.Communication.Resources
{
    public class ChatResource
    {
        public int Id { get; set; }
        public DateTime PostDate { get; set; }
        public int CustomerId { get; set; }
        public CustomerResource Customer { get; set; }
        public int CompanyId { get; set; }
        public CompanyResource Company { get; set; }
    }
}
