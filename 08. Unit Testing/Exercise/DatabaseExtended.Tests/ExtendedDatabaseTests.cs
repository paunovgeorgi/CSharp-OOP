using System;
using ExtendedDatabase;

namespace DatabaseExtended.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database database;
        
        [SetUp]

        public void SetUp()
        {
            Person[] persons =
            {
                new Person(1, "Pesho"),
                new Person(2, "Gosho"),
                new Person(3, "Ivan_Ivan"),
                new Person(4, "Pesho_ivanov"),
                new Person(5, "Gosho_Naskov"),
                new Person(6, "Pesh-Peshov"),
                new Person(7, "Ivan_Kaloqnov"),
                new Person(8, "Ivan_Draganchov"),
                new Person(9, "Asen"),
                new Person(10, "Jivko"),
                new Person(11, "Toshko")
            };

            database = new Database(persons);
        }
        [Test]
        public void DatabaseFindByUsernameMethodShouldBeCaseSensitive()
        {
            string expectedResult = "peShO";
            string actualResult = database.FindByUsername("Pesho").UserName;

            Assert.AreNotEqual(expectedResult, actualResult);
        }

        [Test]

        public void Person_Constructor_ShouldWorkProperly()
        {
            Person person = new Person(1, "name");
            Assert.IsNotNull(person);
            Assert.AreEqual(1, person.Id);
            Assert.AreEqual("name", person.UserName);
        }


        [Test]

        public void Database_Count_ShouldWorkProperly()
        {
            Assert.AreEqual(11, database.Count);
        }

        [Test]
        public void CreatingDatabaseShouldThrowExceptionWhenCountIsMoreThan16()
        {

            Person[] persons =
            {
                new Person(1, "Pesho"),
                new Person(2, "Gosho"),
                new Person(3, "Ivan_Ivan"),
                new Person(4, "Pesho_ivanov"),
                new Person(5, "Gosho_Naskov"),
                new Person(6, "Pesh-Peshov"),
                new Person(7, "Ivan_Kaloqnov"),
                new Person(8, "Ivan_Draganchov"),
                new Person(9, "Asen"),
                new Person(10, "Jivko"),
                new Person(11, "Toshko"),
                new Person(12, "Moshko"),
                new Person(13, "Foshko"),
                new Person(14, "Loshko"),
                new Person(15, "Roshko"),
                new Person(16, "Boshko"),
                new Person(17, "Kokoshko")
            };

            ArgumentException exception = Assert.Throws<ArgumentException>(()
                => database = new Database(persons));

            Assert.AreEqual("Provided data length should be in range [0..16]!", exception.Message);
        }

        [Test]

        public void Database_AddMethod_ShouldWorkCorrectly()
        {
            Person person = new Person(123, "Name");

            database.Add(person);

            Assert.AreEqual(12, database.Count);
        }

        [Test]
        public void AddRangeMethod_ShouldWorkCorrectly()
        {
            Person[] persons =
            {
                new Person(1, "Pesho"),
                new Person(2, "Gosho"),
                new Person(3, "Ivan_Ivan"),
                new Person(4, "Pesho_ivanov"),
                new Person(5, "Gosho_Naskov"),
                new Person(6, "Pesh-Peshov"),
                new Person(7, "Ivan_Kaloqnov"),
                new Person(8, "Ivan_Draganchov"),
                new Person(9, "Asen"),
                new Person(10, "Jivko"),
                new Person(11, "Toshko"),
                new Person(12, "Moshko"),
                new Person(13, "Foshko"),
                new Person(14, "Loshko"),
                new Person(15, "Roshko"),
                new Person(16, "Boshko")
            };

            database = new Database(persons);

            Assert.AreEqual(database.Count, persons.Length);
            Assert.AreEqual(16, persons.Length);
        }

        [Test]
        public void DatabaseAddMethodShouldThrowExceptionIfCountIsMoreThan16()
        {
            Person person1 = new(12, "John");
            Person person2 = new(13, "Paul");
            Person person3 = new(14, "Green");
            Person person4 = new(15, "Brown");
            Person person5 = new(16, "Killer");

            database.Add(person1);
            database.Add(person2);
            database.Add(person3);
            database.Add(person4);
            database.Add(person5);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(()
                => database.Add(new Person(17, "Destroyer")));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", exception.Message);
        }


        [Test]

        public void Database_AddMethod_ShouldThrowException_WhenUserNameOfPersonIsAlreadyPresentInTheList()
        {
            Person current = new Person(4444, "Ivan_Draganchov");

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.Add(current));

            Assert.AreEqual("There is already user with this username!", ex.Message);

        }

        [Test]

        public void Database_AddMethod_ShouldThrowException_WhenIdOfPersonIsAlreadyPresentInTheList()
        {
            Person current = new Person(8, "Rooney");

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.Add(current));

            Assert.AreEqual("There is already user with this Id!", ex.Message);

        }

        [Test]

        public void RemoveMethod_ShouldWorkCorrectly()
        {
            database.Remove();

            Assert.AreEqual(10, database.Count);
        }

        [Test]

        public void RemoveMethod_ShouldThrowException_WhenDatabaseIsEmpty()
        {
            Database data = new();

            Assert.Throws<InvalidOperationException>(()
                => data.Remove());
        }

       
        [TestCase("")]
        [TestCase(null)]

        public void FindUserByName_ShouldThrowException_WhenNameis_NullOrEmpty(string name)
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(()
                => database.FindByUsername(name));

            Assert.AreEqual("Username parameter is null!", ex.ParamName);

        }

        [TestCase("Leo Messi")]

        public void FindUserByName_ShouldThrowException_WhenNameis_False(string name)
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.FindByUsername(name));

            Assert.AreEqual("No user is present by this username!", ex.Message);

        }

        
        [TestCase("Pesho")]
        public void FindUserByName_ShouldWorkCorrectly(string name)
        {
            Assert.AreEqual(name, database.FindByUsername(name).UserName);
        }

        [TestCase(2)]
        public void FindUserById_ShouldWorkCorrectly(int id)
        {
         

            Assert.AreEqual("Gosho", database.FindById(id).UserName);
        }


        [TestCase(-1)]
        [TestCase(-100)]
        public void FindUserById_ShouldThrowException_WhenIdIsNegative(int id)
        {
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => database.FindById(id));
            
            Assert.AreEqual("Id should be a positive number!", ex.ParamName);



        }

        [TestCase(0)]
        [TestCase(100)]
        public void FindUserById_ShouldThrowException_WhenIdIsFalse(int id)
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.FindById(id));

            Assert.AreEqual("No user is present by this ID!", ex.Message);

        }
    }
}