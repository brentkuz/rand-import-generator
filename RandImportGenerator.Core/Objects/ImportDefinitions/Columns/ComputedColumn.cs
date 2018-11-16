namespace RandImportGenerator.Core.Objects.ImportDefinitions.Columns
{
    public class ComputedColumn : ColumnDefinitionBase
    {
        public ComputedColumn(string name) : base(name, ColumnType.Computed)
        {
        }

        public delegate string Calculate(params object[] args);

        public Calculate Calculator { get; set; }
    }
}
