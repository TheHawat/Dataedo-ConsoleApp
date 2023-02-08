using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class DatabaseSchema : ImportedObjectBaseClass
    {
        private List<Table> _entries;
        public DatabaseSchema(string name) {
            Name = name;
            _entries = new List<Table>();
        }

        public void AddTable(Table newTable) {
            _entries.Add(newTable);
        }

        public override void Print() {
            Console.WriteLine($"Database '{Name}' ({_entries.Count} tables)");
            _entries.ForEach(x => x.Print());
        }
    }

    public class Table : ImportedObjectBaseClass
    {
        private string _schema;
        private List<Column> _entries;

        public Table(string name, string schema) {
            Name = name;
            _schema = schema;
            _entries= new List<Column>();
        }

        public void AddColumn(Column newColumn) {
            _entries.Add(newColumn);
        }

        public override void Print() {
            Console.WriteLine($"\tTable '{_schema}.{Name}' ({_entries.Count} columns)");
            _entries.ForEach(x => x.Print());
        }
    }

    public class Column : ImportedObjectBaseClass
    {
        private bool _isNullable;
        private string _dataType;

        public Column(string name, string dataType, string isNullable) {
            _isNullable = isNullable == "1" ? true : false;
            _dataType = dataType;
            Name = name;
        }

        public override void Print() {
            Console.WriteLine($"\t\tColumn '{Name}' with {_dataType} data type {(_isNullable ? "accepts nulls" : "with no nulls")}");
        }
    }

    abstract public class ImportedObjectBaseClass
    {
        public string Name;
        abstract public void Print();
    }
}