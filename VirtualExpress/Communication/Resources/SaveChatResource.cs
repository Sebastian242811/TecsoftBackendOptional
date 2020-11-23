using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.Communication.Resources
{
    public class SaveChatResource
    {
        [Required]
        public DateTime PostDate { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}
