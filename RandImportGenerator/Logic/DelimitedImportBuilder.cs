using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandImportGenerator.Objects;
using RandImportGenerator.Logic.FileWriters;

namespace RandImportGenerator.Logic
{
    public abstract class DelimitedImportBuilder : ImportBuilderBase
    {
        protected char delimiter;

        public DelimitedImportBuilder(string fileExtension, IFileWriter fileWriter) : base(fileExtension, fileWriter)
        {
        }

        public virtual void SetDelimiter(char delimiter)
        {
            this.delimiter = delimiter;
        }
        
    }
}
