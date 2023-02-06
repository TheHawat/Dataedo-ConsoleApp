using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class SchemaPrinter
    {
        private readonly List<DatabaseSchemaEntry> _importedObjects;
        public SchemaPrinter(SchemaReader input) {
            _importedObjects = input.ImportedObjects;
        }

        public void PrintDatabaseSchema() {
            foreach (var database in _importedObjects) {
                if (database.Type != "DATABASE") continue;
                Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");
                PrintChildTablesColumns(database.Type, database.Name);
            }
        }

        private void PrintChildTablesColumns(string type, string name) {
            foreach (var table in _importedObjects) {
                if (table.ParentType != type) continue;
                if (table.ParentName != name) continue;
                Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");
                PrintChildColumns(table.Type, table.Name);
            }
        }

        private void PrintChildColumns(string type, string name) {
            foreach (var column in _importedObjects) {
                if (column.ParentType != type) continue;
                if (column.ParentName != name) continue;
                Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
            }
        }
    }
}
