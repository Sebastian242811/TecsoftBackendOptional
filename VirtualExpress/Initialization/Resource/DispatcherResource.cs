﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.Initialization.Resources
{
    public class DispatcherResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DNI { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }
    }
}
