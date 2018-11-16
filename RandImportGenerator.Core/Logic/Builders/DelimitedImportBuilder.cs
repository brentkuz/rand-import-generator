using RandImportGenerator.Core.Logic.FileWriters;
using RandImportGenerator.Core.Objects.ImportDefinitions;
using RandImportGenerator.Core.Utility.Validation;

namespace RandImportGenerator.Core.Logic.Builders
{
    public abstract class DelimitedImportBuilder : ImportBuilderBase
    {
        public DelimitedImportBuilder(string fileExtension, IFileWriter fileWriter, IValidationHelper validation) : base(fileExtension, fileWriter, validation)
        {
        }

        public virtual void SetDelimiter(char delimiter)
        {
            CheckDefinitionType(typeof(DelimitedImportDefinition),
                string.Format("Import definition must be of type {0} to set delimiter.",
                typeof(DelimitedImportDefinition).ToString()));

            (definition as DelimitedImportDefinition).Delimiter = delimiter;
        }
        
    }
}
