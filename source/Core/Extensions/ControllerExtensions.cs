using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Our.Umbraco.NonProfitFramework.Core.Extensions
{
    public static class ControllerExtensions
    {
        public static string RenderRazorViewToString(this Controller controller, string viewPath,
            object model, ViewDataDictionary viewData = null)
        {
            var tempData = new TempDataDictionary();
            var newViewData = (viewData == null) ? new ViewDataDictionary() : new ViewDataDictionary(viewData);
            newViewData.Model = model;
            var context = controller.ControllerContext;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewPath);
                var viewContext = new ViewContext(context, viewResult.View, newViewData, tempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(context, viewResult.View);
                string renderedView = sw.GetStringBuilder().ToString();
                return renderedView;
            }
        }
    }
}
