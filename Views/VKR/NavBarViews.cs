using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalWork_BD_Test.Views.VKR
{
    public class NavBarViews
    {
        public static string Common => "Common";
        public static string Documents => "Documents";
        public static string BuildVkr => "BuildVkr";
        
        public static string CommonNavClass(ViewContext viewContext) => ViewNavClass(viewContext, Common);
        public static string DocumentsNavClass(ViewContext viewContext) => ViewNavClass(viewContext, Documents);
        public static string BuildVkrNavClass(ViewContext viewContext) => ViewNavClass(viewContext, BuildVkr);
        
        private static string ViewNavClass(ViewContext viewContext, string page)
        {
            var activeView = viewContext.ViewData["ActiveView"] as string
                             ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activeView, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

    }
}