using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Logic.FileWriters
{
    public interface IFileWriter
    {
        void Write(string path, string contents);
    }
}
