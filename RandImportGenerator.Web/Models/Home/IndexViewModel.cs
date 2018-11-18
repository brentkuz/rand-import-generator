using RandImportGenerator.Core;
using RandImportGenerator.Web.Utility.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft;
using Newtonsoft.Json;
using RandImportGenerator.Crosscutting.Utility;
using System.ComponentModel.DataAnnotations;

namespace RandImportGenerator.Web.Models.Home
{
    public class IndexViewModel : ViewModelBase
    {
        public IndexViewModel()
        {
            var type = typeof(ColumnType);
            ColumnTypes = JsonConvert.SerializeObject(Enum.GetValues(type).Cast<int>().ToDictionary(e => e, e => ((ColumnType)e).GetAttribute<DisplayAttribute>().Name));
        }

        //config
        [ClientConfiguration]
        public string ColumnTypes { get; set; }
    }
}