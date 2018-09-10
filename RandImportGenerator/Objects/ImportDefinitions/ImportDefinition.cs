using RandImportGenerator.Objects.ImportDefinitions.Columns;
using RandImportGenerator.Utility.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RandImportGenerator.Objects.ImportDefinitions
{
    public class ImportDefinition
    {
        [CollectionIsNotEmpty(ErrorMessage = "Columns collection may not be empty.")]
        public List<ColumnDefinitionBase> Columns { get; protected set; } = new List<ColumnDefinitionBase>();

        [Required]
        [Range(0, Int32.MaxValue)]
        public int? RowCount { get; set; }
    }
}
