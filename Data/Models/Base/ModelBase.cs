using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace FinalWork_BD_Test.Data.Models.Base
{
    public class ModelBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
