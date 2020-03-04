using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Intermidate;
using FinalWork_BD_Test.Data.Models.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalWork_BD_Test.Controllers
{
    [Authorize]
    public class GecController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;

        public GecController(ApplicationDbContext c, UserManager<User> u)
        {
            _context = c;
            _userManager = u;
        }

        [HttpGet]
        public IActionResult GecMemberProfile()
        {
            ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees.AsNoTracking().AsEnumerable(), "Id", "Name");//.Append(new SelectListItem("Отсутствует", "null", true));
            ViewData["AcademicTitleId"] = new SelectList(_context.AcademicTitles.AsNoTracking().AsEnumerable(), "Id", "Name");//.Append(new SelectListItem("Отсутствует", "null", true));

            return View();
        }

        [HttpPost]
        public IActionResult GecMemberProfile(GecMemberProfile profile)
        {
            profile.UpdatedByObj = null;
            profile.CreatedDate = DateTime.Now;

            _context.GecMemberProfiles.Add(profile);
            _context.SaveChanges();

            return View();
        }

        //[HttpGet]
        //public IActionResult Gec()
        //{
        //    ViewData["MembersId"] = new SelectList(_context.GecMemberProfiles.Where(mp => mp.UpdatedByObj == null), "Id", "FirstNameIP");
        //    ViewData["EducationFormId"] = new SelectList(_context.EducationForms, "Id", "Name");

        //    return View();
        //}

        [HttpGet]
        public IActionResult Gec(Guid gecId)
        {
            GEC gec = _context.GECs.FirstOrDefault(g => g.Id == gecId);
            _context.GecMemberProfiles.Load();
            _context.GecMemberIntermediates.Load();

            SelectList MembersProfiles = new SelectList(_context.GecMemberProfiles.Where(mp => mp.UpdatedByObj == null), "Id", "FirstNameIP");
            ViewData["EducationFormId"] = new SelectList(_context.EducationForms, "Id", "Name");

            if (gec != null)
            {
                if (gec.Members != null)
                    foreach (var member in gec.Members)
                    {
                        var memberListItem = MembersProfiles.FirstOrDefault(mp => mp.Value == member.MemberProfileId.ToString());
                        if (memberListItem != null)
                            memberListItem.Selected = true;
                    }
            }

            ViewData["MembersId"] = MembersProfiles;

            //return View("Gec", gec);
            return View(gec);
        }

        [HttpPost]
        public IActionResult GEC(GEC gec, ICollection<Guid> membersid)
        {
            gec.UpdatedByObj = null;
            gec.CreatedDate = DateTime.Now;

            //TEMP
            gec.Chief = _context.GecMemberProfiles.First(mp => mp.Id == gec.Chief.Id);
            gec.Deputy = _context.GecMemberProfiles.First(mp => mp.Id == gec.Deputy.Id);

            _context.GECs.Add(gec);


            foreach (Guid member in membersid)
            {
                var memberIntermidiate = new GecMemberIntermediate { GecId = gec.Id, MemberProfileId = member };
                _context.GecMemberIntermediates.Add(memberIntermidiate);
                gec.Members.Add(memberIntermidiate);
            }

            _context.SaveChanges();

            return RedirectToAction();
        }
    }
}