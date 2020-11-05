﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.General.Response;

namespace VirtualExpress.Communication.Domain.Services.Responses
{
    public class ChatResponse : BaseResponse<Chat>
    {
        public ChatResponse(Chat resource) : base(resource)
        {
        }

        public ChatResponse(string message) : base(message)
        {
        }
    }
}
