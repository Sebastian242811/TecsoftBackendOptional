﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.General.Response;

namespace VirtualExpress.CompanyManagement.Domain.Services.Responses
{
    public class TerminalResponse : BaseResponse<Terminal>
    {
        public TerminalResponse(Terminal resource) : base(resource)
        {
        }

        public TerminalResponse(string message) : base(message)
        {
        }
    }
}
