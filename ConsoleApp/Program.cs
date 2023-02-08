using System;
using System.IO;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main() {
            ImportAndPrintData();
            Console.ReadLine();
        }

        static void ImportAndPrintData() {
            var DBSchema = new SchemaReader();
            StreamReader FileToImport = new StreamReader("data.csv");
            DBSchema.ImportSchema(FileToImport);
            DBSchema.Print();
        }
    }
}
