using System.Collections.Generic;
using FinalWork_BD_Test.Data.Models;

namespace FinalWork_BD_Test.Areas.Admin.Models
{
    public class GecViewModel
    {
        public IEnumerable<GEC> Gecs { get; set; }
        public PageViewModel PageViewModel { get; set; }

    }
}