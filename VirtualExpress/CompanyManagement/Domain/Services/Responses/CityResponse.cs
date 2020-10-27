using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.General.Response;

namespace VirtualExpress.CompanyManagement.Domain.Services.Responses
{
    public class CityResponse : BaseResponse<City>
    {
        public CityResponse(City resource) : base(resource)
        {
        }

        public CityResponse(string message) : base(message)
        {
        }
    }
}
