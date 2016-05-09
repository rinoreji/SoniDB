using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoniDB;
using System.Configuration;
using System.Text.RegularExpressions;

namespace SoniDBTests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void ConstructorTest_Implements_Disposible()
        {
            var target = new Database();
            Assert.IsInstanceOfType(target, typeof(IDisposable));
        }

        [TestMethod]
        public void ConstructorTest_Default_UsesDefaultConnection()
        {
            var target = new Database();

            Assert.AreEqual("SoniDB", target.ConnectionInfo.DataSource);
        }

        [TestMethod]
        public void ConstructorTest_Default_UsesDefaultConnection_InvalidConnetionString()
        {
            var target = new Database("dummy");

            Assert.AreEqual("SoniDB", target.ConnectionInfo.DataSource);
        }

        [TestMethod]
        public void ConstructorTest_UsesPassedConnection()
        {
            var connString = ConfigurationManager.ConnectionStrings["SoniDBTests_Connection"].ConnectionString;
            var rex = new Regex(@"^DataSource=(.*);*.*$");
            var match = rex.Match(connString);

            var expected = match.Groups[1].Value;
            var target = new Database("SoniDBTests_Connection");

            Assert.AreEqual(expected, target.ConnectionInfo.DataSource);
        }
    }
}
