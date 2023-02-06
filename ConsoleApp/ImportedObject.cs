namespace ConsoleApp
{
    public class ImportedObject : ImportedObjectBaseClass
    {
        public string Schema;
        public string ParentName;
        public string ParentType;
        public string DataType;
        public string IsNullable;
        public double NumberOfChildren = 0;
        public ImportedObject(string line) {
            var values = line.Split(';');
            Type = values[0];
            Name = values[1];
            Schema = values[2];
            ParentName = values[3];
            ParentType = values[4];
            DataType = values[5];
            IsNullable = values[6];
        }
    }
    abstract public class ImportedObjectBaseClass
    {
        public string Name;
        public string Type;
    }
}
