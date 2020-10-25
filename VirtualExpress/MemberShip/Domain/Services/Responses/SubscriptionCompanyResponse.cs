using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.MemberShip.Domain.Model;

namespace VirtualExpress.MemberShip.Domain.Services.Responses
{
    public class SubscriptionCompanyResponse : BaseResponse<SubscriptionCompany>
    {
        public SubscriptionCompanyResponse(SubscriptionCompany resource) : base(resource)
        {
        }

        public SubscriptionCompanyResponse(string message) : base(message)
        {
        }
    }
}
