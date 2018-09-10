using RandImportGenerator.Logic.FileWriters;
using RandImportGenerator.Objects.ImportDefinitions;
using RandImportGenerator.Utility.Validation;

namespace RandImportGenerator.Logic.Builders
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
