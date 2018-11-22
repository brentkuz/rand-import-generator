using RandImportGenerator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RandImportGenerator.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class CSVBuilderController : Controller
    {
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult GetColumnTemplate(ColumnType type)
        {
            var viewPath = string.Format("{0}{1}{2}.{3}", 
                Constants.ColumnTemplatesViewPath, type.ToString(), Constants.PartialViewEnding, Constants.ViewExtension); 
            return PartialView(viewPath);
        }

        public JsonResult Test()
        {
            var dict = new Dictionary<string, string>()
            {
                {"1", "One" },
                {"2", "Two" }
            };

            return Json(dict, JsonRequestBehavior.AllowGet);
        }
    }
}