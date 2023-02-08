using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ConsoleApp
{
    public class SchemaReader
    {
        private List<DatabaseSchema> _allDBSchema;
        public List<DatabaseSchema> AllDBSchema => _allDBSchema;
        private Queue<(Table tab, string parentName)> _importedTable;
        private List<(Column col, string parentName)> _importedColumn;

        public SchemaReader() {
            _allDBSchema = new List<DatabaseSchema>();
            _importedTable = new Queue<(Table tab, string parentName)>();
            _importedColumn = new List<(Column col, string parentName)>();
        }

        public string CleanOneLine(string line) {
            line = line.Replace(" ", "");
            line = line.Replace(Environment.NewLine, "");
            return line;
        }

        public void ImportSchema(StreamReader fileToImport) {
            while (!fileToImport.EndOfStream) {
                var line = fileToImport.ReadLine();
                if (!ValidateLine(line)) continue;
                ProcessLine(line);
            }
            ProcessTables();
        }

        public void ProcessTables() {
            while (_importedTable.Count > 0) {
                AddTableToDB(ProcessTable());
            }
        }

        public bool ValidateLine(string importedLine) {
            if (string.IsNullOrEmpty(importedLine)) return false;
            int SemicolonValidator = importedLine.Count(x => x == ';');
            if (SemicolonValidator != 6) return false;
            return true;
        }

        public void Print() {
            _allDBSchema.ForEach(x => x.Print());
        }

        private (Table, string) ProcessTable() {
            (Table CurrentTable, string CurrentName) = _importedTable.Dequeue();
            for (int i = 0; i < _importedColumn.Count; i++) {
                if (_importedColumn[i].parentName == CurrentTable.Name) {
                    CurrentTable.AddColumn(_importedColumn[i].col);
                    _importedColumn.RemoveAt(i);
                    i--;
                }
            }
            return (CurrentTable, CurrentName);
        }

        private void ProcessLine(string line) {
            line = CleanOneLine(line);
            string[] values = line.Split(';');
            values[0] = values[0].ToLower();
            if (values[0] == "database") {
                _allDBSchema.Add(new DatabaseSchema(values[1]));
                return;
            }
            if (values[0] == "table") {
                _importedTable.Enqueue((new Table(values[1], values[2]), values[3]));
                return;
            }
            if (values[0] == "column") {
                _importedColumn.Add((new Column(values[1], values[5], values[6]), values[3]));
                return;
            }
        }

        private void AddTableToDB((Table currentTable, string currentName) input) {
            for (int i = 0; i < _allDBSchema.Count; i++) {
                if (_allDBSchema[i].Name == input.currentName) {
                    _allDBSchema[i].AddTable(input.currentTable);
                }
            }
        }
    }
}
