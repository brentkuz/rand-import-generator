using RandImportGenerator.Core.Logic.FileWriters;
using RandImportGenerator.Core.Utility.Validation;
using System;

namespace RandImportGenerator.Core.Logic.Builders
{
    public class ImportBuilderFactory : IImportBuilderFactory
    {
        private IValidationHelper validation;

        public ImportBuilderFactory(IValidationHelper validation)
        {
            this.validation = validation;
        }
        public ImportBuilderBase GetImportBuilder(FileType type)
        {
            switch(type)
            {
                case FileType.CSV:
                    return new CSVImportBuilder(validation);
                default:
                    throw new ArgumentException("File type is not supported");
            }
        }
    }
}
