using System.IO;

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
