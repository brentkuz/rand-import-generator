using RandImportGenerator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace RandImportGenerator.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class CSVBuilderController : Controller
    {
        [HttpPost]
        public HttpResponseMessage CreateFile()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public FileResult DownloadFile(Guid id)
        {
            return null;
        }
    }
}