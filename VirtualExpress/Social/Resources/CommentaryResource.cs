using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.Social.Resources
{
    public class CommentaryResource
    {
        public int Id { get; set; }
        public string Opinion { get; set; }
        public int Valoration { get; set; }
        public int CustomerId { get; set; }
        public int CompanyId { get; set; }
    }
}
