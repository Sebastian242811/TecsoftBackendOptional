﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.ShipProvincial.Domain.Models;

namespace VirtualExpress.Initialization.Domain.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Number { get; set; }
        public DateTime Brithday { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public IList<Package> Packages { get; set; } = new List<Package>();
        public IList<SubscriptionCustomer> Subscriptions { get; set; } = new List<SubscriptionCustomer>();
        //Subscription
    }
}
