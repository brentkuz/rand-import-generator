namespace RandImportGenerator.Core.Logic.FileWriters
{
    public interface IWriter
    {
        void Write(string path, string contents);
        bool DirectoryExists(string path);
    }
}
