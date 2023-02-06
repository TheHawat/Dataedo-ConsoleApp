using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    public class DataReader
    {
        private List<ImportedObject> _importedObjects;
        public List<ImportedObject> ImportedObjects => _importedObjects;
        public DataReader() {
            _importedObjects = new List<ImportedObject>();
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

        public string CleanOneLine(string line) {
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

        public bool ValidateLine(string importedLine) {
            if (string.IsNullOrEmpty(importedLine)) return false;
            int SemicolonValidator = importedLine.Count(x => x == ';');
            if (SemicolonValidator != 6) return false;
            return true;
        }
    }
}
