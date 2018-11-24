using System;
using System.IO;

namespace RandImportGenerator.Core.Logic.FileWriters
{
    public class FileWriter : IWriter
    {
        private string outputPath;
        public string OutputPath
        {
            get { return outputPath; }
            set
            {
                var dirPath = Path.GetDirectoryName(value);
                if(!Directory.Exists(dirPath))
                    throw new SystemException(string.Format("The path {0} does not exist.", dirPath));
                outputPath = value;
            }
        }

        public void Write(string contents)
        {
            File.WriteAllText(OutputPath, contents);
        }
    }
}
