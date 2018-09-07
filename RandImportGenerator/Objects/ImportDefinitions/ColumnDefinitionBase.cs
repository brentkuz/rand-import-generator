using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Objects.ImportDefinitions
{
    public abstract class ColumnDefinitionBase
    {
        public ColumnDefinitionBase(string name, ColumnType type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; protected set; }
        public ColumnType Type { get; protected set; }
        public int ColumnOrder { get; set;}
    }


}
