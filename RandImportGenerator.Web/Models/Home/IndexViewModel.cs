using RandImportGenerator.Web.Utility.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RandImportGenerator.Web.Models.Home
{
    public class IndexViewModel : ViewModelBase
    {
        //config
        [ClientConfiguration]
        public string ConfigValue1 { get; set; }
    }
}