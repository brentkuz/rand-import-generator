namespace RandImportGenerator.Core.Objects.ImportDefinitions.Columns
{
    public class AutoIncrementedColumn : ColumnDefinitionBase
    {
        public AutoIncrementedColumn() : base(ColumnType.AutoIncremented) { }
        public AutoIncrementedColumn(string name) : base(name, ColumnType.AutoIncremented)
        {
        }
        public int StartingSequenceNumber { get; set; }
        public int IncrementValue { get; set; } = 1;

    }
}
