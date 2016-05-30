using System;
using System.ComponentModel.DataAnnotations;

namespace Smartville.Models
{
    public class City : BaseModel
    {
        [Required]
        [StringLength(200)]
        public String Name { get; set; }

        [Required]
        public State State { get; set; }
    }
}
