using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalWork_BD_Test.Data.Migrations
{
    public class TestSeed
    {
        public static void Seed(DbContext Context)
        {
            var appContext = Context as ApplicationDbContext;
            if (appContext == null)
                return;
            
        }
    }
}
