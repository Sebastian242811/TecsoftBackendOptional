using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.MemberShip.Resource
{
    public class SaveTypeOfCurrentResource
    {
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
    }
}
