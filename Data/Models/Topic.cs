using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalWork_BD_Test.Data.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace FinalWork_BD_Test.Data.Models
{
    public class Topic : HistoricalModelBase<Topic>
    {
        [Required]
        [MaxLength(256)]
        [Display(Name = "Тема")]
        public string Title { get; set; }

        [HiddenInput]
        public Guid? SuperVisorId { get; set; }

        [ForeignKey("SuperVisorId")]
        public virtual User Supervisor { get; set; }
    }
}
