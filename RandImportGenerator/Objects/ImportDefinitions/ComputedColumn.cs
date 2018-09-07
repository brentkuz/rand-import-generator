using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Objects.ImportDefinitions
{
    public class ComputedColumn : ColumnDefinitionBase
    {
        public ComputedColumn(string name) : base(name, ColumnType.Computed)
        {
        }

        public delegate string Calculate(params object[] args);

        public Calculate Calculator { get; set; }
    }
}
