﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;
using FinalWork_BD_Test.Data.Models.Data;
using FinalWork_BD_Test.Data.Models.Intermidate;
using FinalWork_BD_Test.Data.Models.Profiles;

namespace FinalWork_BD_Test.Data.Models
{
    public class GEC : HistoricalModelBase<GEC>
    {
        // ToDo: move to separete table
        [Required(ErrorMessage = "Введите номер специальности")]
        [Display(Name = "Специальность")]
        public string Specialty { get; set; }

        public Guid EducationFormId { get; set; }
        [ForeignKey("EducationFormId")]
        [Required(ErrorMessage = "Выберите форму обучения")]
        [Display(Name = "Форма обучения")]
        public virtual EducationForm EducationForm { get; set; }

        /// <summary>Председатель</summary>
        public Guid ChiefId { get; set; }
        [ForeignKey("ChiefId")]
        [Required(ErrorMessage = "Выберите председателя комиссии")]
        [Display(Name = "Председатель комиссии")]
        public virtual GecMemberProfile Chief { get; set; }

        /// <summary>Заместитель председателя</summary>
        public Guid DeputyId { get; set; }
        [ForeignKey("DeputyId")]
        [Required(ErrorMessage = "Выберите заместителя председателя")]
        [Display(Name = "Заместитель председателя")]
        public virtual GecMemberProfile Deputy { get; set; }

        public virtual ICollection<GecMemberIntermediate> Members { get; set; }

        //ToDo: Change queries to use this field
        [DefaultValue(false)]
        public bool IsArchived { get; set; }
    }
}
