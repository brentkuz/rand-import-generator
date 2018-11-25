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
using RandImportGenerator.Core.Utility.CustomAttributes;

namespace RandImportGenerator.Web.Models.Home
{
    public class IndexViewModel : ViewModelBase
    {
        public IndexViewModel()
        {
            var columnTypesToInclude = Enum.GetValues(typeof(ColumnType)).Cast<ColumnType>()
                .Where(x => ((ColumnType)x).GetAttribute<ClientIgnore>() == null).Cast<int>();
            ColumnTypes = JsonConvert.SerializeObject(columnTypesToInclude.ToDictionary(e => e, e => ((ColumnType)e).GetAttribute<DisplayAttribute>()?.Name));
            ColumnKeyNameMap = JsonConvert.SerializeObject(columnTypesToInclude.ToDictionary(e => e, e => ((ColumnType)e).ToString()));

            var quoteTypesToInclude = Enum.GetValues(typeof(QuoteType)).Cast<QuoteType>()
                .Where(x => ((QuoteType)x).GetAttribute<ClientIgnore>() == null).Cast<int>()
                .ToDictionary(e => e, e => ((QuoteType)e).GetAttribute<DisplayAttribute>()?.Name);
            QuoteTypes = JsonConvert.SerializeObject(quoteTypesToInclude);
        }

        //config
        [ClientConfiguration]
        public string ColumnTypes { get; set; }
        [ClientConfiguration]
        public string ColumnKeyNameMap { get; set; }
        [ClientConfiguration]
        public string QuoteTypes { get; set; }
    }
}