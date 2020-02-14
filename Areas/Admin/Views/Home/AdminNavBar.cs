using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalWork_BD_Test.Areas.Admin.Views.Home
{
    public static class AdminNavBar
    {
        public static string Index => "Index";

        public static string AllUsers => "AllUsers";
        public static string FindUser => "FindUser";
            
        public static string AllRoles => "AllRoles";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);
        public static string AllUsersNavClass(ViewContext viewContext) => PageNavClass(viewContext, AllUsers);
        public static string FindUserNavClass(ViewContext viewContext) => PageNavClass(viewContext, FindUser);
        public static string AllRolesNavClass(ViewContext viewContext) => PageNavClass(viewContext, AllRoles);
        
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActiveView"] as string
                             ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}

