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
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public int ChatId { get; set; }
        [Required]
        public int CustomerServiceEmployeeId { get; set; }
        [Required]
        public int CustomerId { get; set; }
    }
}
