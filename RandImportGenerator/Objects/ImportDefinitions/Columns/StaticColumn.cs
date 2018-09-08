namespace RandImportGenerator.Objects.ImportDefinitions.Columns
{
    public class StaticColumn : ColumnDefinitionBase
    {
        public StaticColumn(string name) : base(name, ColumnType.Static)
        {
        }

        public string Value { get; set; }
    }
}
