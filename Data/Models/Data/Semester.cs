﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;

namespace FinalWork_BD_Test.Data.Models.Data
{
    /// <summary>
    /// Учебный семестр
    /// </summary>
    public class Semester : ModelBase
    {
        /// <summary>
        /// Название семестра
        /// </summary>
        [MaxLength(5)]
        public string Name { get; set; }
    }
}
