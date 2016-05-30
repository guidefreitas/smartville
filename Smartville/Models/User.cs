using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smartville.Models
{
    public class User
    {
        public long Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public String Email { get; set; }
        
        public String Password { get; set; }

        [StringLength(100)]
        public String AuthToken { get; set; }

        [Required]
        public virtual UserType UserType { get; set; }
    }
}
