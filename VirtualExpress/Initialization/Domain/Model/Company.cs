using System;
using System.Collections.Generic;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.Initialization.Domain.Model;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.ShipProvincial.Controller;
using VirtualExpress.Social.Domain.Models;

namespace VirtualExpress.Initialization.Domain.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Ruc { get; set; }
        public IList<Terminal> Terminals { get; set; } = new List<Terminal>();
        //Subscription
        public IList<Chat> Chats { get; set; } = new List<Chat>();
        public IList<Message> Messages { get; set; } = new List<Message>();
        public IList<SubscriptionCompany> Subscriptions { get; set; } = new List<SubscriptionCompany>();
        public IList<Commentary> Commentaries { get; set; } = new List<Commentary>();
        public IList<Dispatcher> Dispatchers { get; set; } = new List<Dispatcher>();
    }
}
