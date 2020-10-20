using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.Social.Domain.Models
{
    public class Commentary
    {
        public int Id { get; set; }
        public string Opinion { get; set; }
        public int Valoration { get; set; }
    }
}
