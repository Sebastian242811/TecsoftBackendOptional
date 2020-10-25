using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.MemberShip.Domain.Model;

namespace VirtualExpress.MemberShip.Domain.Services.Responses
{
    public class SubscriptionCustomerResponse : BaseResponse<SubscriptionCustomer>
    {
        public SubscriptionCustomerResponse(SubscriptionCustomer resource) : base(resource)
        {
        }

        public SubscriptionCustomerResponse(string message) : base(message)
        {
        }
    }
}
