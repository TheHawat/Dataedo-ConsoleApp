namespace ConsoleApp
{
    [TestClass]
    public class ConsoleAppUnitTests
    {
        [TestMethod]
        public void Validator_EmptyString() {
            SchemaReader TestSchemaReader = new SchemaReader();
            string TestString = "";
            bool Result = TestSchemaReader.ValidateLine(TestString);
            Assert.IsTrue(!Result);
        }
        [TestMethod]
        public void Validator_NotEnoughFields() {
            SchemaReader TestSchemaReader = new SchemaReader();
            string TestString = "Table;ShipMethod;Purchasing;AdventureWorks2016_EXTDatabase;NULL;\r\n";
            bool Result = TestSchemaReader.ValidateLine(TestString);
            Assert.IsTrue(!Result);
        }
        [TestMethod]
        public void Validator_ValidLine() {
            SchemaReader TestSchemaReader = new SchemaReader();
            string TestString = "Table;ProductCostHistory;Production;AdventureWorks2016_EXT;DATABASE;NULL;\r\n";
            bool Result = TestSchemaReader.ValidateLine(TestString);
            Assert.IsTrue(Result);
        }
        [TestMethod]
        public void CleanOneLine_CorrectData() {
            SchemaReader TestSchemaReader = new SchemaReader();
            string TestString = "Too Many Spaces In This Line";
            string ExpectedResult = "TooManySpacesInThisLine";
            TestString = TestSchemaReader.CleanOneLine(TestString);
            Console.WriteLine(TestString);
            Assert.IsTrue(TestString == ExpectedResult);
        }
        [TestMethod]
        public void WholeProcess() {
            StreamReader FileToImport = new StreamReader("TestData_CorrectFile.csv");
            var Reader = new SchemaReader();
            Reader.ImportSchema(FileToImport);
            var Printer = new SchemaPrinter(Reader);
            var StringWriter = new StringWriter();
            Console.SetOut(StringWriter);
            Printer.PrintDatabaseSchema();
            string ExpectedResult = File.ReadAllText("TestData_CorrectFile_Result.txt");
            Assert.AreEqual(ExpectedResult, StringWriter.ToString());
        }
        [TestMethod]
        public void WholeProcessWithBadData() {
            StreamReader FileToImport = new StreamReader("TestData_BadFile.csv");
            var Reader = new SchemaReader();
            Reader.ImportSchema(FileToImport);
            var Printer = new SchemaPrinter(Reader);
            var StringWriter = new StringWriter();
            Console.SetOut(StringWriter);
            Printer.PrintDatabaseSchema();
            string ExpectedResult = File.ReadAllText("TestData_CorrectFile_Result.txt");
            Assert.AreEqual(ExpectedResult, StringWriter.ToString());
        }
        [TestMethod]
        public void ImportData() {
            string filePath =  "TestData_BadFile.csv";
            StreamReader FileToImport = new StreamReader(filePath);
            var Reader = new SchemaReader();
            Reader.ImportSchema(FileToImport);
            Assert.AreEqual(Reader.ImportedObjects[0].NumberOfChildren, 3);
            Assert.AreEqual(Reader.ImportedObjects[1].Type, "TABLE");
            Assert.AreEqual(Reader.ImportedObjects[5].IsNullable, "0");
        }
    }
}