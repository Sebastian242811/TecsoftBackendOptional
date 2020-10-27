using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.Initialization.Domain.Model;

namespace VirtualExpress.Initialization.Domain.Services.Responses
{
    public class DealerResponse : BaseResponse<Dealer>
    {
        public DealerResponse(Dealer resource) : base(resource)
        {
        }

        public DealerResponse(string message) : base(message)
        {
        }
    }
}
