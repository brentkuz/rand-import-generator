namespace RandImportGenerator.Logic.FileWriters
{
    public interface IFileWriter
    {
        void Write(string path, string contents);
    }
}
