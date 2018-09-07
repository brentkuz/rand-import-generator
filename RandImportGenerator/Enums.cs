using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator
{
    public enum FileType
    {
        CSV = 1
    }
    public enum ColumnType
    {
        AutoIncremented = 1,
        Randomized = 2,
        Dependent = 3,
        Static = 4,
        Computed = 5
    }
}
