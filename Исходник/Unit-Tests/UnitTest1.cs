using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Unit_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string connectionString = "Data Source=:memory:";
            SQLiteConnection con = new SQLiteConnection(connectionString);
            con.Open();

            string createTableQuery = "CREATE TABLE Вакансии (Наименование TEXT, Работодатель TEXT, Зарплата REAL, УровеньОбразования TEXT, СтатусВакансии TEXT)";
            SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, con);
            createTableCommand.ExecuteNonQuery();

            string insertDataQuery = "INSERT INTO Вакансии (Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии) VALUES ('Test Vacancy', 'Test Employer', 1000, 'Higher Education', 'Open')";
            SQLiteCommand insertDataCommand = new SQLiteCommand(insertDataQuery, con);
            insertDataCommand.ExecuteNonQuery();

            DataTable dt = new DataTable();
            string query = $"select Наименование, Работодатель, Зарплата, УровеньОбразования, СтатусВакансии from Вакансии";
            SQLiteCommand command = new SQLiteCommand(query, con);
            SQLiteDataAdapter ad = new SQLiteDataAdapter(command);
            ad.Fill(dt);

            Assert.IsNotNull(dt);
            Assert.AreEqual(1, dt.Rows.Count);
            Assert.AreEqual("Test Vacancy", dt.Rows[0]["Наименование"]);
            Assert.AreEqual("Test Employer", dt.Rows[0]["Работодатель"]);
            Assert.AreEqual(1000.0, dt.Rows[0]["Зарплата"]);
            Assert.AreEqual("Higher Education", dt.Rows[0]["УровеньОбразования"]);
            Assert.AreEqual("Open", dt.Rows[0]["СтатусВакансии"]);
        }

        [TestMethod]
        public void TestGetConnectionString_ReturnsValidConnectionString()
        {
            string connectionString = GetConnectionString();

            Assert.IsNotNull(connectionString);
            Assert.IsTrue(connectionString.Contains("Data Source = emplcenter.db"));
            Assert.IsTrue(connectionString.Contains("Version = 3"));
        }

        public string GetConnectionString()
        {
            return $@"Data Source = emplcenter.db; Version = 3";
        }

        
    }
}


