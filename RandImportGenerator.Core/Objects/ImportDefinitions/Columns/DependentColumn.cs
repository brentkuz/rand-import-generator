using System.Collections.Generic;

namespace RandImportGenerator.Core.Objects.ImportDefinitions.Columns
{
    public class DependentColumn : ColumnDefinitionBase
    {
        public DependentColumn(string name) : base(name, ColumnType.Dependent)
        {
        }

        public delegate string Calculate(object value);

        public Calculate Calculator { get; set; }

        public Dictionary<object, string> Map { get; set; }

        public string DependsOn { get; set; }

 
    }
}
