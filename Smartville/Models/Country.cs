using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smartville.Models
{
    public class Country : BaseModel
    {
        [Required]
        [StringLength(100)]
        public String Name { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}
