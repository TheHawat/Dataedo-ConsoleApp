namespace ConsoleApp
{
    using System;
    internal class Program
    {
        static void Main() {
            ImportAndPrintData();
            Console.ReadLine();
        }
        static void ImportAndPrintData() {
            var reader = new DataReader();
            reader.ImportData("data.csv");
            reader.PrintData();
        }
    }
}
