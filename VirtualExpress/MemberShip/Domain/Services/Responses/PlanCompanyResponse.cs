using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.MemberShip.Domain.Model;

namespace VirtualExpress.MemberShip.Domain.Services.Responses
{
    public class PlanCompanyResponse : BaseResponse<PlanCompany>
    {
        public PlanCompanyResponse(PlanCompany resource) : base(resource)
        {
        }

        public PlanCompanyResponse(string message) : base(message)
        {
        }
    }
}
