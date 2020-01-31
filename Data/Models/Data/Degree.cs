using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;

namespace FinalWork_BD_Test.Data.Models
{
    /// <summary>
    /// Степень высшего образования
    /// </summary>
    public class Degree : ModelBase
    {
        /// <summary>
        /// Название cтепени
        /// </summary>
        public string Name { get; set; }
    }
}
