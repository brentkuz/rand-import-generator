using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Objects.ImportDefinitions
{
    public class ImportDefinition
    {

        public ImportDefinition()
        {
        }

        public string Name { get; set; }

        public List<ColumnDefinitionBase> Columns { get; set; } = new List<ColumnDefinitionBase>();
    }
}
