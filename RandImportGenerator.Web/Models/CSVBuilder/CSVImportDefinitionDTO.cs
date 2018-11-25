using RandImportGenerator.Core;
using RandImportGenerator.Core.Objects.ImportDefinitions.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RandImportGenerator.Web.Models.CSVBuilder
{
    public class CSVImportDefinitionDTO
    {
        public int RowCount { get; set; }
        public AutoIncrementedColumn[] AutoIncremented { get; set; }
        public DependentColumn[] Dependent { get; set; }
        public RandomizedColumn[] Randomized { get; set; }
        public StaticColumn[] Static { get; set; }        
    }
}