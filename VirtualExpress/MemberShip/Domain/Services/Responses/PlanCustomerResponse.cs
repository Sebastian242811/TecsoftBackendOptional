using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.MemberShip.Domain.Model;

namespace VirtualExpress.MemberShip.Domain.Services.Responses
{
    public class PlanCustomerResponse : BaseResponse<PlanCustomer>
    {
        public PlanCustomerResponse(PlanCustomer resource) : base(resource)
        {
        }

        public PlanCustomerResponse(string message) : base(message)
        {
        }
    }
}
