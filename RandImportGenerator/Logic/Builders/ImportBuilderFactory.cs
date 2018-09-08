using RandImportGenerator.Logic.FileWriters;
using System;

namespace RandImportGenerator.Logic.Builders
{
    public class ImportBuilderFactory : IImportBuilderFactory
    {
        private IFileWriter fileWriter;

        public ImportBuilderFactory(IFileWriter fileWriter)
        {
            this.fileWriter = fileWriter;
        }
        public ImportBuilderBase GetImportBuilder(FileType type)
        {
            switch(type)
            {
                case FileType.CSV:
                    return new CSVImportBuilder(fileWriter);
                default:
                    throw new ArgumentException("File type is not supported");
            }
        }
    }
}
