using System;
using System.Collections.Generic;
using System.Linq;
using FinalWork_BD_Test.Areas.Admin.Models;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Intermidate;
using FinalWork_BD_Test.Data.Models.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalWork_BD_Test.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class GecController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;

        public GecController(ApplicationDbContext c, UserManager<User> u)
        {
            _context = c;
            _userManager = u;
        }

        public ActionResult Index(int page = 1)
        {
            int pageSize = 7;

            List<GEC> source = _context.GECs
                .Include(t => t.EducationForm)
                .Include(t => t.Chief)
                .Include(t => t.Deputy)
                .Where(t => t.UpdatedByObj == null && t.IsArchived == false)
                .ToList();
            
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
 
            var pageViewModel = new PageViewModel(count, page, pageSize);
            var viewModel = new GecViewModel
            {
                PageViewModel = pageViewModel,
                Gecs = items
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GecMemberProfile(Guid gecMemberId = default, Guid gecId = default)
        {
            GecMemberProfile memberProfile = null;
            
            if (gecMemberId == Guid.Empty)
            {
                ViewData["AcademicDegreeId"] =
                    new SelectList(_context.AcademicDegrees.AsNoTracking().AsEnumerable(), "Id",
                        "Name"); //.Append(new SelectListItem("Отсутствует", "null", true));

                ViewData["AcademicTitleId"] =
                    new SelectList(_context.AcademicTitles.AsNoTracking().AsEnumerable(), "Id",
                        "Name"); //.Append(new SelectListItem("Отсутствует", "null", true));
            }
            else
            {
                memberProfile = _context.GecMemberProfiles
                    .Include(g => g.AcademicDegree)
                    .Include(g => g.AcademicTitle)
                    .FirstOrDefault(g => g.Id == gecMemberId && g.UpdatedByObj == null && g.IsArchived == false);

                ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees.AsNoTracking().AsEnumerable(),
                    "Id", "Name", memberProfile.AcademicDegree.Id);

                ViewData["AcademicTitleId"] = new SelectList(_context.AcademicTitles.AsNoTracking().AsEnumerable(),
                    "Id", "Name", memberProfile.AcademicTitle.Id);
            }

            ViewData["GecId"] = gecId;
            
            return View(memberProfile);
        }

        [HttpPost]
        public IActionResult GecMemberProfile(GecMemberProfile profile)
        {
            var oldProfile = _context.GecMemberProfiles
                .Include(m => m.AcademicDegree)
                .Include(m => m.AcademicTitle)
                .FirstOrDefault(m => m.Id == profile.Id && m.UpdatedByObj == null && m.IsArchived == false);

            if (oldProfile != null) 
                oldProfile.UpdatedByObj = profile;
            
            profile.Id = Guid.Empty;
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
        public IActionResult Gec(Guid gecId = default)
        {
            GEC gec = _context.GECs.FirstOrDefault(g => g.Id == gecId);
            _context.GecMemberProfiles.Load();
            _context.GecMemberIntermediates.Load();

            var membersProfiles = new SelectList(_context.GecMemberProfiles
                .Where(mp => mp.UpdatedByObj == null && mp.IsArchived == false), 
                "Id", "FirstNameIP");
            ViewData["EducationFormId"] = new SelectList(_context.EducationForms, "Id", "Name");

            if (gec != null)
            {
                if (gec.Members != null)
                    foreach (var member in gec.Members)
                    {
                        var memberListItem = membersProfiles.FirstOrDefault(mp => mp.Value == member.MemberProfileId.ToString());
                        if (memberListItem != null)
                            memberListItem.Selected = true;
                    }
                
                ViewData["MembersId"] = membersProfiles;

                //return View("Gec", gec);
                return View(gec);
            }
            else
            {            
                ViewData["MembersId"] = membersProfiles;

                return View();
            }

        }

        [HttpPost]
        public IActionResult GEC(GEC gec, ICollection<Guid> membersid)
        {
            var oldGec = _context.GECs.FirstOrDefault(g => g.Id == gec.Id && g.UpdatedByObj == null && g.IsArchived == false);
            if (oldGec != null)
                oldGec.UpdatedByObj = gec;

            gec.Id = Guid.Empty;
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