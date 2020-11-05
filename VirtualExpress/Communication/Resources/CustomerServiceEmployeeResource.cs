using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Resources;

namespace VirtualExpress.Communication.Resources
{
    public class CustomerServiceEmployeeResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TerminalId { get; set; }

        public TerminalResource Terminal { get; set; }

    }
}
