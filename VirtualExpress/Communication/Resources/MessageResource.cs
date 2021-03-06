﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Resource;

namespace VirtualExpress.Communication.Resources
{
    public class MessageResource
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int ChatId { get; set; }

        public ChatResource Chat { get; set; }

        public int CompanyId { get; set; }

        public int CustomerId { get; set; }

    }
}
