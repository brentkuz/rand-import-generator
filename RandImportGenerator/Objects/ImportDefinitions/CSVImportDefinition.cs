using System.ComponentModel.DataAnnotations;

namespace RandImportGenerator.Objects.ImportDefinitions
{
    public class CSVImportDefinition : DelimitedImportDefinition
    {
        [Required]
        public char QuoteCharacter { get; set; }
    }
}
