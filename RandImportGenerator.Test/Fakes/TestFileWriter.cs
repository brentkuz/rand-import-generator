using RandImportGenerator.Core.Logic.FileWriters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandImportGenerator.Test.Fakes
{
    public class TestFileWriter : IWriter
    {
        public string InMemoryFileContents { get; set; }
        

        public void Write(string contents)
        {
            InMemoryFileContents = contents;
        }
    }
}
