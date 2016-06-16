using System;
using System.Collections;
using System.Collections.Generic;
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

        public virtual ICollection<Institute> Institutes { get; set; }
    }
}
