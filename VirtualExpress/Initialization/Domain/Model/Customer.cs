using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.MemberShip.Domain.Model;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.Social.Domain.Models;

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
        public IList<Chat> Chats { get; set; } = new List<Chat>();
        public IList<Message> Messages { get; set; } = new List<Message>();
        public IList<Commentary> Commentaries { get; set; } = new List<Commentary>();
    }
}
