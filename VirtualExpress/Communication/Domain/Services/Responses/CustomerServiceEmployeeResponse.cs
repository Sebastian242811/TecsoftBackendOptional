using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.General.Response;

namespace VirtualExpress.Communication.Domain.Services.Responses
{
    public class CustomerServiceEmployeeResponse : BaseResponse<CustomerServiceEmployee>
    {
        public CustomerServiceEmployeeResponse(CustomerServiceEmployee resource) : base(resource)
        {
        }

        public CustomerServiceEmployeeResponse(string message) : base(message)
        {
        }
    }
}
