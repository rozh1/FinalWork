using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;
using FinalWork_BD_Test.Data.Models.Data;
using FinalWork_BD_Test.Data.Models.Profiles;

namespace FinalWork_BD_Test.Data.Models
{
    public class VKR : HistoricalModelBase<VKR>
    {
        public UserProfile StudentUP { get; set; }
        public StudentProfile StudentSP { get; set; }
        public Topic Topic { get; set; }
        public UserProfile SupervisorUP { get; set; }
        public LecturerProfile SupervisorLP { get; set; }
        
        /// <summary>
        /// Рецензент, для магистров
        /// </summary>
        public UserProfile ReviewerUP { get; set; }

        [Required]
        [MaxLength(4), MinLength(4)]
        public ulong Year { get; set; }

        public Guid SemesterId { get; set; }
        public virtual Semester Semester { get; set; }

    }
}
