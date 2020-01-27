using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalWork_BD_Test.Models
{
    public class StudentProfileView
    {
        public User User { get; set; }

        public string FirstNameRP { get; set; }

        public string SecondNameRP { get; set; }

        public string MiddleNameRP { get; set; }

        public string FirstNameDP { get; set; }

        public string SecondNameDP { get; set; }

        public string MiddleNameDP { get; set; }

        public string Degree { get; set; }

        public string Gender { get; set; }

        public string EducationForm { get; set; }

        [MaxLength(5)]
        public string Group { get; set; }

        public ushort GraduateYear { get; set; }

        public string GraduateSemester { get; set; }
    }
}
