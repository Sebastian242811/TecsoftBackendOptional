using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpress.Social.Resources
{
    public class SaveCommentaryResource
    {
        [Required]
        [MaxLength(250)]
        public string Opinion { get; set; }

        [Required]
        public int Valoration { get; set; }
    }
}
