using System;
using NUnit.Framework.Interfaces;

namespace Database.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;
        [SetUp]

        public void SetUp()
        {
           database = new(1, 2);
        }

        [TestCase(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16})]
        [TestCase(new int[] {1, 2, 3, 4, 5, 6, 7, 8})]

        public void CreateDatabaseConstructorIsWorking(int[] data)
        {
            Database currentData = new(data);
            int[] result = currentData.Fetch();
            Assert.AreEqual(data, result);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 })]

        public void DatabaseShouldNotExceed16(int[] data)
        {
            Assert.Throws<InvalidOperationException>(()
                => database = new(data));
        }

        [Test]

        public void CreateDatabaseInstructorIsWorkingProperly()
        {
            int[] expectedResult = new[] { 1, 2 };

            Assert.AreEqual(expectedResult, database.Fetch());
        }


        [Test]

        public void CreatingDatabaseCountShouldBeCorrect()
        {
            int expectedResult = 2;

            Assert.AreEqual(expectedResult, database.Count);
        }

        [Test]

        public void AddDataBaseMethorIsWorkingCorrectly()
        {
            int expecterResult = 3;

            database.Add(16);

            Assert.AreEqual(expecterResult, database.Count);
        }

        [Test]

        public void RemoveDatabaseMethodIsWorkingCorrectly()
        {
            int expectedResult = 1;

            database.Remove();

            Assert.AreEqual(expectedResult, database.Count);
        }

        [Test]

        public void RemoveMethodEmptyCollectionCheck()
        {
            database.Remove();
            database.Remove();

            Assert.Throws<InvalidOperationException>(()
                => database.Remove(), "The collection is empty!");
        }
    }
}
