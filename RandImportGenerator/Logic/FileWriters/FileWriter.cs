using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Logic.FileWriters
{
    public class FileWriter : IFileWriter
    {
        public void Write(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }
    }
}
