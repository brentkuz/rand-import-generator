namespace RandImportGenerator.Core.Objects.ImportDefinitions.Columns
{
    public class StaticColumn : ColumnDefinitionBase
    {
        public StaticColumn() : base(ColumnType.Static) { }
        public StaticColumn(string name) : base(name, ColumnType.Static)
        {
        }

        public string Value { get; set; }
    }
}
