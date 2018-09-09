using System.IO;

namespace RandImportGenerator.Logic.FileWriters
{
    public class FileWriter : IFileWriter
    {
        public bool DirectoryExists(string path)
        {
            var dirPath = Path.GetDirectoryName(path);
            return Directory.Exists(dirPath);
        }

        public void Write(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }
    }
}
