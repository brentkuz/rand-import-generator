namespace RandImportGenerator.Core.Objects.ImportDefinitions.Columns
{
    public abstract class ColumnDefinitionBase
    {
        public ColumnDefinitionBase(string name, ColumnType type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; protected set; }
        public ColumnType Type { get; protected set; }
        public int ColumnOrder { get; set;}
    }


}
