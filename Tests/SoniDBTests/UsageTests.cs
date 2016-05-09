using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoniDB;
using System.Collections.Generic;
using System.Reflection;

namespace SoniDBTests
{
    [TestClass]
    public class UsageTests
    {
        public class TestClass
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        [TestInitialize]
        public void TestInit()
        {
            System.IO.Directory.Delete("SoniDB", true);
        }

        [TestMethod]
        public void GetCollectionReturnsObject()
        {
            using (var db = new Database())
            {
                IEnumerable<TestClass> classes = db.GetCollection<TestClass>();
                Assert.IsNotNull(classes);
            }
        }

        [TestMethod]
        public void SavesChangesToDBOnlyOnCallingSave()
        {
            using (var db = new Database())
            {
                var collection = db.GetCollection<TestClass>();
                collection.Add(new TestClass { Age = 1, Name = "ds" });

                var result = db.GetCollection<TestClass>();
                Assert.AreEqual(0, result.Count);

                collection.Save();

                result = db.GetCollection<TestClass>();
                Assert.AreEqual(1, result.Count);
            }
        }

        [TestMethod]
        public void SavesMultipleItemsToDBOnlyOnCallingSave()
        {
            using (var db = new Database())
            {
                var collection = db.GetCollection<TestClass>();
                collection.Add(new TestClass { Age = 1, Name = "ds1" });
                collection.Add(new TestClass { Age = 2, Name = "ds2" });
                collection.Add(new TestClass { Age = 3, Name = "ds3" });
                collection.Add(new TestClass { Age = 4, Name = "ds4" });
                collection.Add(new TestClass { Age = 5, Name = "ds5" });

                var result = db.GetCollection<TestClass>();
                Assert.AreEqual(0, result.Count);

                collection.Save();

                result = db.GetCollection<TestClass>();
                Assert.AreEqual(5, result.Count);
            }
        }
    }
}
