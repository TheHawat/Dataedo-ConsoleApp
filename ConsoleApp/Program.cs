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
            var Reader = new SchemaReader();
            StreamReader FileToImport = new StreamReader("data.csv");
            Reader.ImportSchema(FileToImport);
            var Printer = new SchemaPrinter(Reader);
            Printer.PrintDatabaseSchema();
        }
    }
}
