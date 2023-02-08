using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class DatabaseSchema : ImportedObject
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
}
