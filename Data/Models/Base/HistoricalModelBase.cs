﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalWork_BD_Test.Data.Models.Base
{
    public class HistoricalModelBase  <T> : ModelBase where T: HistoricalModelBase<T> 
    {
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        
        [ForeignKey("UpdatedBy")]
        public virtual T UpdatedByObj { get; set; }
    }
}
