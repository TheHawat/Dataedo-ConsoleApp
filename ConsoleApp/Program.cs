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
            var Reader = new DataReader();
            StreamReader FileToImport = new StreamReader("data.csv");
            Reader.ImportData(FileToImport);
            var Printer = new DataPrinter(Reader);
            Printer.PrintData();
        }
    }
}
