using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.General.Response;

namespace VirtualExpress.CompanyManagement.Domain.Services.Responses
{
    public class ShipTerminalResponse : BaseResponse<ShipTerminal>
    {
        public ShipTerminalResponse(ShipTerminal resource) : base(resource)
        {
        }

        public ShipTerminalResponse(string message) : base(message)
        {
        }
    }
}
