namespace ConsoleApp
{
    [TestClass]
    public class ConsoleAppUnitTests
    {
        [TestMethod]
        public void TestValidator_EmptyString() {
            DataReader HawatTest = new DataReader();
            string TestString = "";
            bool Result = HawatTest.ValidateLine(TestString);
            Assert.IsTrue(!Result);
        }
        [TestMethod]
        public void TestValidator_NotEnoughFields() {
            DataReader HawatTest = new DataReader();
            string TestString = "Table;ShipMethod;Purchasing;AdventureWorks2016_EXTDatabase;NULL;\r\n";
            bool Result = HawatTest.ValidateLine(TestString);
            Assert.IsTrue(!Result);
        }
        [TestMethod]
        public void TestValidator_ValidLine() {
            DataReader HawatTest = new DataReader();
            string TestString = "Table;ProductCostHistory;Production;AdventureWorks2016_EXT;DATABASE;NULL;\r\n";
            bool Result = HawatTest.ValidateLine(TestString);
            Assert.IsTrue(Result);
        }
        [TestMethod]
        public void TestCleanOneLine() {
            DataReader HawatTest = new DataReader();
            string TestString = "Too Many Spaces In This Line";
            string ExpectedResult = "TooManySpacesInThisLine";
            TestString = HawatTest.CleanOneLine(TestString);
            Console.WriteLine(TestString);
            Assert.IsTrue(TestString == ExpectedResult);
        }
    }
}