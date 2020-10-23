using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.CompanyManagement.Resources
{
    public class SaveTerminalResource
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Adress { get; set; }
        [Required]
        public int CityId { get; set; }
    }
}
