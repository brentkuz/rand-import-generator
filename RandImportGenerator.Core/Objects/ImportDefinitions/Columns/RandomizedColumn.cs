namespace RandImportGenerator.Core.Objects.ImportDefinitions.Columns
{
    public class RandomizedColumn : ColumnDefinitionBase
    {
        public RandomizedColumn() : base(ColumnType.Randomized) { }
        public RandomizedColumn(string name) : base(name, ColumnType.Randomized)
        {
        }
        public string[] RandomizationOptions { get; set; }
    }
}
