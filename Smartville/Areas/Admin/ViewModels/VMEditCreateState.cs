using Smartville.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smartville.Areas.Admin.ViewModels
{
    public class VMEditCreateState
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Informe a sigla com duas letras")]
        [MaxLength(2)]
        [Display(Name= "Sigla")]
        public String Code { get; set; }

        [Required(ErrorMessage = "Informe o país")]
        [Display(Name = "País")]
        public long CountryId { get; set; }

        public List<Country> Countries { get; set; }
    }

}