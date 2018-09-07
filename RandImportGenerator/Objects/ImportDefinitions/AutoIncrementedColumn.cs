using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Objects.ImportDefinitions
{
    public class AutoIncrementedColumn : ColumnDefinitionBase
    {
        public AutoIncrementedColumn(string name) : base(name, ColumnType.AutoIncremented)
        {
        }
        public int StartingSequenceNumber { get; set; }
        public int IncrementValue { get; set; } = 1;

    }
}
