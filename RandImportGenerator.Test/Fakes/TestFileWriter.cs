using RandImportGenerator.Core.Logic.FileWriters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Test.Fakes
{
    public class TestFileWriter : IFileWriter
    {
        public string InMemoryFileContents { get; set; }

        public bool DirectoryExists(string path)
        {
            return true;
        }

        public void Write(string path, string contents)
        {
            InMemoryFileContents = contents;
        }
    }
}
