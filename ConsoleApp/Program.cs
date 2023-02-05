namespace ConsoleApp
{
    using System;
    using System.IO;

    internal class Program
    {
        static void Main() {
            ImportAndPrintData();
            Console.ReadLine();
        }
        static void ImportAndPrintData() {
            var reader = new DataReader();
            StreamReader FileToImport = new StreamReader("data.csv");
            reader.ImportData(FileToImport);
            reader.PrintData();
        }
    }
}
