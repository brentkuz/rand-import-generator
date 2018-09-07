using RandImportGenerator.Logic.FileWriters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Logic
{
    public interface IImportBuilderFactory
    {
        ImportBuilderBase GetImportBuilder(FileType type);
    }
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
