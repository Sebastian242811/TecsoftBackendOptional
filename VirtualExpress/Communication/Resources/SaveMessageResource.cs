using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.Communication.Resources
{
    public class SaveMessageResource
    {
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public int ChatId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int CustomerId { get; set; }
    }
}
