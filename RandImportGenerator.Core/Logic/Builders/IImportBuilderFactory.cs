namespace RandImportGenerator.Core.Logic.Builders
{
    public interface IImportBuilderFactory
    {
        ImportBuilderBase GetImportBuilder(FileType type);
    }
}
