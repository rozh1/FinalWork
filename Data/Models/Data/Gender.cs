using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;

namespace FinalWork_BD_Test.Data.Models.Data
{
    /// <summary>
    /// Пол
    /// </summary>
    public class Gender : ModelBase
    {
        /// <summary>
        /// Название пола
        /// </summary>
        [MaxLength(7)]
        public string Name { get; set; }
    }
}
