using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class Table : ImportedObject
    {
        private string _schema;
        private List<Column> _entries;

        public Table(string name, string schema) {
            Name = name;
            _schema = schema;
            _entries = new List<Column>();
        }

        public void AddColumn(Column newColumn) {
            _entries.Add(newColumn);
        }

        public override void Print() {
            Console.WriteLine($"\tTable '{_schema}.{Name}' ({_entries.Count} columns)");
            _entries.ForEach(x => x.Print());
        }
    }
}
