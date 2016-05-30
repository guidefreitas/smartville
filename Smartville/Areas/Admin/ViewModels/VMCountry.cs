using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smartville.Areas.Admin.ViewModels
{
    public class VMCountry
    {
        public long Id { get; set; }

        [Required]
        public String Name { get; set; }
    }
}
