namespace RandImportGenerator.Logic.Builders
{
    public interface IImportBuilderFactory
    {
        ImportBuilderBase GetImportBuilder(FileType type);
    }
}
