using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.Communication.Resources
{
    public class ChatResource
    {
        public int Id { get; set; }

        public DateTime PostDate { get; set; }

        public IList<MessageResource> Messages { get; set; } = new List<MessageResource>();

    }
}
