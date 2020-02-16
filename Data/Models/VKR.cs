﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;
using FinalWork_BD_Test.Data.Models.Data;
using FinalWork_BD_Test.Data.Models.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        public Guid? SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        [Display(Name = "Семестр")]
        public virtual Semester Semester { get; set; }

        
        public static SelectList GetSupervisorList(ApplicationDbContext context, UserManager<User> userManager, UserProfile supervisor = null)
        {
            var users = userManager.GetUsersInRoleAsync("Supervisor").Result;
            context.UserProfiles.Load();

            Dictionary<Guid, string> dc = new Dictionary<Guid, string>();
            foreach (var user in users)
            {
                var userProfile = user.UserProfiles?.FirstOrDefault(up => up.UpdatedByObj == null);
                if (userProfile == null)
                    continue;
                dc.Add(userProfile.Id, $"{userProfile.SecondNameIP} {userProfile.FirstNameIP[0]}.{userProfile.MiddleNameIP[0]}.");
            }
            if (supervisor != null)
                return new SelectList(dc, "Key", "Value", supervisor.Id);
            return new SelectList(dc, "Key", "Value");
        }

        /// <summary>
        /// Равны ли обе ВКР
        /// </summary>
        /// <param name="beforeVkr"></param>
        /// <param name="afterVkr"></param>
        /// <returns></returns>
        public static bool EqualsVkr(VKR beforeVkr, VKR afterVkr)
        {
            if (beforeVkr.Topic.Title == afterVkr.Topic.Title)
            {
                afterVkr.Topic = beforeVkr.Topic;
                if (beforeVkr.SupervisorUPId == afterVkr.SupervisorUPId && beforeVkr.SemesterId == afterVkr.SemesterId &&
                    beforeVkr.Year == afterVkr.Year)
                    return true;
            }

            return false;
        }

    }
}
