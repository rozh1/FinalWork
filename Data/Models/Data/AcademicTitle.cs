using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;

namespace FinalWork_BD_Test.Data.Models.Data
{
    /// <summary>
    /// Учёное звание
    /// </summary>
    public class AcademicTitle : ModelBase
    {
        /// <summary>
        /// Название звания
        /// </summary>
        public string Name { get; set; }
    }
}
