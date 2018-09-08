using RandImportGenerator.Objects.ImportDefinitions.Columns;
using System.Collections.Generic;

namespace RandImportGenerator.Objects.ImportDefinitions
{
    public class ImportDefinition
    {
        public List<ColumnDefinitionBase> Columns { get; set; } = new List<ColumnDefinitionBase>();
    }
}
