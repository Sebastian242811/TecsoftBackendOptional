using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.Initialization.Resource
{
    public class CustomerResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Number { get; set; }
        public DateTime Brithday { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
