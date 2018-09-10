using RandImportGenerator.Logic.FileWriters;
using RandImportGenerator.Utility.Validation;
using System;

namespace RandImportGenerator.Logic.Builders
{
    public class ImportBuilderFactory : IImportBuilderFactory
    {
        private IFileWriter fileWriter;
        private IValidationHelper validation;

        public ImportBuilderFactory(IFileWriter fileWriter, IValidationHelper validation)
        {
            this.fileWriter = fileWriter;
            this.validation = validation;
        }
        public ImportBuilderBase GetImportBuilder(FileType type)
        {
            switch(type)
            {
                case FileType.CSV:
                    return new CSVImportBuilder(fileWriter, validation);
                default:
                    throw new ArgumentException("File type is not supported");
            }
        }
    }
}
