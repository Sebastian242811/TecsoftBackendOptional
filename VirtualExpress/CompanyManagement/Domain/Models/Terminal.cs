﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.CompanyManagement.Domain.Models
{
    public class Terminal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public IList<ShipTerminal> ShipTerminalso { get; set; } = new List<ShipTerminal>();
        public IList<ShipTerminal> ShipTerminalsd { get; set; } = new List<ShipTerminal>();
        public IList<Package> Packages { get; set; } = new List<Package>();
    }
}
