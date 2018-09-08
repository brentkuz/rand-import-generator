namespace RandImportGenerator.Objects.ImportDefinitions.Columns
{
    public class RandomizedColumn : ColumnDefinitionBase
    {
        public RandomizedColumn(string name) : base(name, ColumnType.Randomized)
        {
        }
        public string[] RandomizationOptions { get; set; }
    }
}
