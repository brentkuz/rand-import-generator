using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Objects.ImportDefinitions
{
    public class RandomizedColumn : ColumnDefinitionBase
    {
        public RandomizedColumn(string name) : base(name, ColumnType.Randomized)
        {
        }
        public string[] RandomizationOptions { get; set; }
    }
}
