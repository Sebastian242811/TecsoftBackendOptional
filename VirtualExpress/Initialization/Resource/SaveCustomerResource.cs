using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.Initialization.Resource
{
    public class SaveCustomerResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(15)]
        public string Username { get; set; }

        [Required]
        [MaxLength(9)]
        public string Number { get; set; }


        public DateTime Brithday { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
        public string Password { get; set; }

        [Required]
        public int CityId { get; set; }
    }
}
