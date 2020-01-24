using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;

namespace FinalWork_BD_Test.Data.Models.Data
{
    /// <summary>
    /// Форма обучения
    /// </summary>
    public class EducationForm : ModelBase
    {
        /// <summary>
        /// Название формы обучения
        /// </summary>
        [MaxLength(10)]
        public string Name { get; set; }
    }
}
