namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    public class DataReader
    {
        private List<ImportedObject> _importedObjects;
        public DataReader() {
            _importedObjects = new List<ImportedObject>();
        }

        public void PrintData() {
            foreach (var database in _importedObjects) {
                if (database.Type != "DATABASE") continue;
                Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");
                PrintTables(database);
            }
        }

        private void PrintTables(ImportedObject database) {
            foreach (var table in _importedObjects) {
                if (table.ParentType != database.Type) continue;
                if (table.ParentName != database.Name) continue;
                Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");
                PrintColumns(table);
            }
        }

        private void PrintColumns(ImportedObject table) {
            foreach (var column in _importedObjects) {
                if (column.ParentType != table.Type) continue;
                if (column.ParentName != table.Name) continue;
                Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
            }
        }

        private void ProcessData() {
            RemoveAllSpacesAndNewLines();
            CalculateChildrenNumbers();
        }

        private void CalculateChildrenNumbers() {
            for (int i = 0; i < _importedObjects.Count(); i++) {
                for (int j = 0; j < _importedObjects.Count(); j++) {
                    if (_importedObjects[j].ParentType != _importedObjects[i].Type) continue;
                    if (_importedObjects[j].ParentName != _importedObjects[i].Name) continue;
                    _importedObjects[i].NumberOfChildren++;
                }
            }
        }

        private void RemoveAllSpacesAndNewLines() {
            foreach (var importedObject in _importedObjects) {
                importedObject.Type = CleanOneLine(importedObject.Type).ToUpper();
                importedObject.Name = CleanOneLine(importedObject.Name);
                importedObject.Schema = CleanOneLine(importedObject.Schema);
                importedObject.ParentName = CleanOneLine(importedObject.ParentName);
                importedObject.ParentType = CleanOneLine(importedObject.ParentType).ToUpper();
            }
        }

        private string CleanOneLine(string line) {
            line = line.Replace(" ", "");
            line = line.Replace(Environment.NewLine, "");
            return line;
        }

        public void ImportData(StreamReader fileToImport) {
            while (!fileToImport.EndOfStream) {
                var line = fileToImport.ReadLine();
                if (!ValidateLine(line)) continue;
                _importedObjects.Add(new ImportedObject(line));
            }
            ProcessData();
        }

        private bool ValidateLine(string importedLine) {
            if (string.IsNullOrEmpty(importedLine)) return false;
            int SemicolonValidator = importedLine.Count(x => x == ';');
            if (SemicolonValidator != 6) return false;
            return true;
        }
    }

    class ImportedObject : ImportedObjectBaseClass
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
    abstract class ImportedObjectBaseClass
    {
        public string Name;
        public string Type;
    }
}
