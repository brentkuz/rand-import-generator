using System.ComponentModel.DataAnnotations;

namespace RandImportGenerator.Core.Objects.ImportDefinitions
{
    public class CSVImportDefinition : DelimitedImportDefinition
    {
        [Required]
        public char QuoteCharacter { get; set; }
    }
}
