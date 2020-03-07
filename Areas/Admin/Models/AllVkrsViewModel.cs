using System.Collections.Generic;
using FinalWork_BD_Test.Data.Models;

namespace FinalWork_BD_Test.Areas.Admin.Models
{
    public class AllVkrsViewModel
    {
        public IEnumerable<VKR> Vkrs { get; set; }
        public PageViewModel PageViewModel { get; set; }

    }
}