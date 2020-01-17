using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinalWork_BD_Test.Models;
using FinalWork_BD_Test.Data;

namespace FinalWork_BD_Test.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)

        public ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            //_logger = logger;
            _context = context;
        }

        
        public IActionResult Index()
        {
            //ViewBag.id = _context.
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<string> Thesis(Thesis thesis)
        {
            _context.Add(thesis);
            await _context.SaveChangesAsync();

            return thesis.Topic;
        }
    }
}
