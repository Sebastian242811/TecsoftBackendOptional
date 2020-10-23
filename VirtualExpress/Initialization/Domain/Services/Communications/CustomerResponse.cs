using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.Initialization.Domain.Model;

namespace VirtualExpress.Initialization.Domain.Services.Communications
{
    public class CustomerResponse : BaseResponse<Customer>
    {
        public CustomerResponse(Customer resource) : base(resource)
        {
        }

        public CustomerResponse(string message) : base(message)
        {
        }
    }
}
