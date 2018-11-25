using RandImportGenerator.Core.Utility.CustomAttributes;
using RandImportGenerator.Crosscutting.Utility;
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
    public enum QuoteType
    {
        [Display(Name = "None")]
        [AlternateValue(null)]
        None = 1,
        [Display(Name = "Double")]
        [AlternateValue('"')]
        Double = 2
    }

    public static class EnumExtensions
    {
        public static char ToChar(this QuoteType type)
        {
            var value = type.GetAttribute<AlternateValueAttribute>().Value;
            return (char)(value != null ? value : default(char));
        }
    }
}
