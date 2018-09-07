using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Objects.ImportDefinitions
{
    public class DependentColumn : ColumnDefinitionBase
    {
        public DependentColumn(string name) : base(name, ColumnType.Dependent)
        {
        }

        public delegate string Calculate(object value);

        public Calculate Calculator { get; set; }

        public string DependsOn { get; set; }

 
    }
}
