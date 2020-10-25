using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.MemberShip.Resource
{
    public class SavePlanCustomerResource
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public double Cost { get; set; }
        [Required]
        public int TypeOfCurrentId { get; set; }
    }
}
