using RandImportGenerator.Logic.FileWriters;
using RandImportGenerator.Objects.ImportDefinitions;

namespace RandImportGenerator.Logic.Builders
{
    public abstract class DelimitedImportBuilder : ImportBuilderBase
    {
        public DelimitedImportBuilder(string fileExtension, IFileWriter fileWriter) : base(fileExtension, fileWriter)
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
