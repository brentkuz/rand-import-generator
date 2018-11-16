using System.ComponentModel.DataAnnotations;

namespace RandImportGenerator.Core.Objects.ImportDefinitions
{
    public class DelimitedImportDefinition : ImportDefinition
    {
        [Required]
        public char? Delimiter { get; set; }
    }
}
