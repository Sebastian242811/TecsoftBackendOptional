using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.MemberShip.Model.Model;

namespace VirtualExpress.MemberShip.Model.Services.Responses
{
    public class TypeOfCurrentResponse : BaseResponse<TypeOfCurrent>
    {
        public TypeOfCurrentResponse(TypeOfCurrent resource) : base(resource)
        {
        }

        public TypeOfCurrentResponse(string message) : base(message)
        {
        }
    }
}
