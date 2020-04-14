using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalWork_BD_Test.Views.VKR
{
    public class NavBarViews
    {
        public static string Common => "Common";
        public static string Documents => "Documents";
        public static string DocumentsForms => "DocumentsForms";
        public static string BuildVkr => "BuildVkr";
        public static string MainDocuments => "MainDocuments";
        public static string OtherDocuments => "OtherDocuments";


        public static string CommonNavClass(ViewContext viewContext) => ViewNavClass(viewContext, Common);
        public static string DocumentsNavClass(ViewContext viewContext) => ViewNavClass(viewContext, Documents);
        public static string DocumentsFormsNavClass(ViewContext viewContext) => ViewNavClass(viewContext, DocumentsForms);
        public static string BuildVkrNavClass(ViewContext viewContext) => ViewNavClass(viewContext, BuildVkr);
        public static string MainDocumentsNavClass(ViewContext viewContext) => ViewNavClass(viewContext, MainDocuments);
        public static string OtherDocumentsNavClass(ViewContext viewContext) => ViewNavClass(viewContext, OtherDocuments);
        
        private static string ViewNavClass(ViewContext viewContext, string page)
        {
            var activeView = viewContext.ViewData["ActiveView"] as string
                             ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activeView, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

    }
}