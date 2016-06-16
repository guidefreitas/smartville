using Smartville.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smartville.Areas.Admin.ViewModels
{
    public class VMEditCreateCity
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        public String Name { get; set; }


        [Required(ErrorMessage = "Informe o estado")]
        [Display(Name = "Estado")]
        public long StateId { get; set; }

        public List<State> States { get; set; }
    }

}