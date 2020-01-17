using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinalWork_BD_Test.Models
{
    public class Thesis
    {
        [Required]
        [Display(Name = "Название темы")]
        public string Topic;

        [Required]
        [Display(Name = "Научный руководитель")]
        public string Teacher;
    }
}
