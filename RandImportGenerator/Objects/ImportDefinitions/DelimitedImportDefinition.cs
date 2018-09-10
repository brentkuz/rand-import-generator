using System.ComponentModel.DataAnnotations;

namespace RandImportGenerator.Objects.ImportDefinitions
{
    public class DelimitedImportDefinition : ImportDefinition
    {
        [Required]
        public char? Delimiter { get; set; }
    }
}
