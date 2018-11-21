using RandImportGenerator.Core.Utility.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace RandImportGenerator.Core
{
    public enum FileType
    {
        CSV = 1
    }
    public enum ColumnType
    {
        [Display(Name = "Auto Incremented")]
        AutoIncremented = 1,
        [Display(Name = "Randomized")]
        Randomized = 2,
        [Display(Name = "Dependent")]
        Dependent = 3,
        [Display(Name = "Static")]
        Static = 4,
        [Display(Name = "Computed")]
        [ClientIgnore]
        Computed = 5
    }
   
}
