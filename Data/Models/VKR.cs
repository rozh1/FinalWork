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
        public Guid? StudentUPId { get; set; }
        [ForeignKey("StudentUPId")]
        public UserProfile StudentUP { get; set; }

        public Guid? StudentSPId { get; set; }
        [ForeignKey("StudentSPId")]
        public StudentProfile StudentSP { get; set; }

        public Guid? TopicId { get; set; }
        [ForeignKey("TopicId")]
        public Topic Topic { get; set; }

        public Guid? SupervisorUPId { get; set; }
        [ForeignKey("SupervisorUPId")]
        public UserProfile SupervisorUP { get; set; }

        public Guid? SupervisorLPId { get; set; }
        [ForeignKey("SupervisorLPId")]
        public LecturerProfile SupervisorLP { get; set; }

        /// <summary>
        /// Рецензент, для магистров
        /// </summary>
        public Guid? ReviewerUPId { get; set; }
        [ForeignKey("ReviewerUPId")]
        public UserProfile ReviewerUP { get; set; }

        [Required]
        [MaxLength(4), MinLength(4)]
        [Display(Name = "Год")]
        public ulong Year { get; set; }

        [Required]
        [Display(Name = "Семестр")]
        public Guid? SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; }

    }
}
