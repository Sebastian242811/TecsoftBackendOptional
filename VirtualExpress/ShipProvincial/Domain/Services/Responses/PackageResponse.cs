using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Response;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.ShipProvincial.Domain.Services.Responses
{
    public class PackageResponse : BaseResponse<Package>
    {
        public PackageResponse(Package resource) : base(resource)
        {
        }

        public PackageResponse(string message) : base(message)
        {
        }
    }
}
