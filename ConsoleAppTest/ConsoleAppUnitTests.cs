namespace ConsoleApp
{
    [TestClass]
    public class ConsoleAppUnitTests
    {
        [TestMethod]
        public void Validator_EmptyString() {
            DataReader HawatTest = new DataReader();
            string TestString = "";
            bool Result = HawatTest.ValidateLine(TestString);
            Assert.IsTrue(!Result);
        }
        [TestMethod]
        public void Validator_NotEnoughFields() {
            DataReader HawatTest = new DataReader();
            string TestString = "Table;ShipMethod;Purchasing;AdventureWorks2016_EXTDatabase;NULL;\r\n";
            bool Result = HawatTest.ValidateLine(TestString);
            Assert.IsTrue(!Result);
        }
        [TestMethod]
        public void Validator_ValidLine() {
            DataReader HawatTest = new DataReader();
            string TestString = "Table;ProductCostHistory;Production;AdventureWorks2016_EXT;DATABASE;NULL;\r\n";
            bool Result = HawatTest.ValidateLine(TestString);
            Assert.IsTrue(Result);
        }
        [TestMethod]
        public void CleanOneLine_CorrectData() {
            DataReader HawatTest = new DataReader();
            string TestString = "Too Many Spaces In This Line";
            string ExpectedResult = "TooManySpacesInThisLine";
            TestString = HawatTest.CleanOneLine(TestString);
            Console.WriteLine(TestString);
            Assert.IsTrue(TestString == ExpectedResult);
        }
        [TestMethod]
        public void WholeProcess() {
            StreamReader FileToImport = new StreamReader("TestData_CorrectFile.csv");
            var Reader = new DataReader();
            Reader.ImportData(FileToImport);
            var Printer = new DataPrinter(Reader);
            var StringWriter = new StringWriter();
            Console.SetOut(StringWriter);
            Printer.PrintData();
            string ExpectedResult = File.ReadAllText("TestData_CorrectFile_Result.txt");
            Assert.AreEqual(ExpectedResult, StringWriter.ToString());
        }
        [TestMethod]
        public void WholeProcessWithBadData() {
            StreamReader FileToImport = new StreamReader("TestData_BadFile.csv");
            var Reader = new DataReader();
            Reader.ImportData(FileToImport);
            var Printer = new DataPrinter(Reader);
            var StringWriter = new StringWriter();
            Console.SetOut(StringWriter);
            Printer.PrintData();
            string ExpectedResult = File.ReadAllText("TestData_CorrectFile_Result.txt");
            Assert.AreEqual(ExpectedResult, StringWriter.ToString());
        }
        [TestMethod]
        public void ImportData() {
            StreamReader FileToImport = new StreamReader("TestData_BadFile.csv");
            var Reader = new DataReader();
            Reader.ImportData(FileToImport);
            Assert.AreEqual(Reader.ImportedObjects[0].NumberOfChildren, 3);
            Assert.AreEqual(Reader.ImportedObjects[1].Type, "TABLE");
            Assert.AreEqual(Reader.ImportedObjects[5].IsNullable, "0");
        }
    }
}