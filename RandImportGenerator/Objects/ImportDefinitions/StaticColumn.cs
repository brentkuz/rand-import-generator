using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Objects.ImportDefinitions
{
    public class StaticColumn : ColumnDefinitionBase
    {
        public StaticColumn(string name) : base(name, ColumnType.Static)
        {
        }

        public string Value { get; set; }
    }
}
