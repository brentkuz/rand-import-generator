using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RandImportGenerator.Web.Models
{
    public class TempDataWrapper
    {
        public TempDataWrapper(TempDataDictionary tempData)
        {
            Handle = Guid.NewGuid();
            TempData = tempData;
        }
        public Guid Handle { get; private set; }
        public TempDataDictionary TempData { get; set; }
    }
}