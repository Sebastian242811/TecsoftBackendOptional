using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.Initialization.Domain.Model;

namespace VirtualExpress.Initialization.Domain.Services.Communications
{
    public class CompanyResponse : BaseResponse<Company>
    {
        public CompanyResponse(Company resource) : base(resource)
        {
        }

        public CompanyResponse(string message) : base(message)
        {
        }
    }
}
