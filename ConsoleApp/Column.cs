using System;

namespace ConsoleApp
{
    public class Column : ImportedObject
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
}
