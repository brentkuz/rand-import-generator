namespace RandImportGenerator.Core.Objects.ImportDefinitions.Columns
{
    public abstract class ColumnDefinitionBase
    {
        public ColumnDefinitionBase(ColumnType type)
        {
            Type = type;
        }
        public ColumnDefinitionBase(string name, ColumnType type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; set; }
        public ColumnType Type { get; protected set; }
        public int ColumnOrder { get; set;}
    }


}
