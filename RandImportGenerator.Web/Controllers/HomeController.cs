using RandImportGenerator.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RandImportGenerator.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new IndexViewModel());
        }

    }

}