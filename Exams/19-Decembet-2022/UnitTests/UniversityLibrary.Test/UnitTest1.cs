namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System.Text;

    public class Tests
    {
        private TextBook textBook;
        private UniversityLibrary uniLibrary;


        [SetUp]
        public void Setup()
        {
            textBook = new TextBook("LOTR", "Tokens", "Fantasy");
            uniLibrary = new UniversityLibrary();
        }

        [Test]
        public void TextBook_Constructor_IsWorkingProperly()
        {
            Assert.NotNull(textBook);
            textBook.InventoryNumber = 1;
            textBook.Holder = "idk";
            Assert.AreEqual("LOTR", textBook.Title);
            Assert.AreEqual("Tokens", textBook.Author);
            Assert.AreEqual("Fantasy", textBook.Category);
            Assert.AreEqual(1, textBook.InventoryNumber);
            Assert.AreEqual("idk", textBook.Holder);
        }


        [Test]

        public void TextBook_ToStringMethod_WorksProperly()
        {
            textBook.InventoryNumber = 1;
            textBook.Holder = "idk";
            StringBuilder result = new StringBuilder();

            result.AppendLine("Book: LOTR - 1");
            result.AppendLine("Category: Fantasy");
            result.AppendLine("Author: Tokens");

            Assert.AreEqual(result.ToString().TrimEnd(), textBook.ToString());
        }

        [Test]

        public void UniversityLibrary_Contructor_WorksProperly()
        {
            Assert.NotNull(uniLibrary);
        }

        [Test]

        public void UniversityLibrary_AddMethod_WorksCorrectly()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Book: LOTR - 1");
            result.AppendLine("Category: Fantasy");
            result.AppendLine("Author: Tokens");

            Assert.IsEmpty(uniLibrary.Catalogue);
            Assert.AreEqual(0, textBook.InventoryNumber);
           Assert.AreEqual(result.ToString().TrimEnd(),uniLibrary.AddTextBookToLibrary(textBook));
            Assert.IsNotEmpty(uniLibrary.Catalogue);
            Assert.AreEqual(1, textBook.InventoryNumber);
        }


        [Test]

        public void UniversityLibrary_LoanTextBook_LoansToNewStudent()
        {
            uniLibrary.AddTextBookToLibrary(textBook);
            Assert.AreEqual("LOTR loaned to Neo.",uniLibrary.LoanTextBook(1, "Neo"));
            Assert.AreEqual("Neo", textBook.Holder);
        }


        [Test]

        public void UniversityLibrary_LoanTextBook_IsNotReturned()
        {
            textBook.Holder = "Neo";
            uniLibrary.AddTextBookToLibrary(textBook);
            Assert.AreEqual("Neo still hasn't returned LOTR!", uniLibrary.LoanTextBook(1, "Neo"));
        }

        [Test]


        public void UniversityLibrary_ReturnBook_WorksProperly()
        {
            textBook.Holder = "Neo";
            uniLibrary.AddTextBookToLibrary(textBook);
            Assert.AreEqual("Neo", textBook.Holder);
            Assert.AreEqual("LOTR is returned to the library.", uniLibrary.ReturnTextBook(1));
            Assert.IsEmpty(textBook.Holder);
        }
    }
}