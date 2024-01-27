namespace Book.Tests
{
    using System;

    using NUnit.Framework;

    public class Tests
    {

        private Book book;

        [SetUp]

        public void Setup()
        {
            book = new Book("LOTR", "Tolkien");
        }

        [Test]

        public void Book_Constructor_WorksProperly()
        {
            Assert.NotNull(book);
            Assert.AreEqual("LOTR", book.BookName);
            Assert.AreEqual("Tolkien", book.Author);
            Assert.AreEqual(0, book.FootnoteCount);
        }


        [TestCase("")]
        [TestCase(null)]

        public void BookName_ThrowsEception_WhenNullOrEmpty(string name)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => book = new Book(name, "Tolkien"));
            Assert.AreEqual("Invalid BookName!", ex.Message);
        }

        [TestCase("")]
        [TestCase(null)]

        public void BookAuthor_ThrowsEception_WhenNullOrEmpty(string author)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => book = new Book("LOTR", author));
            Assert.AreEqual("Invalid Author!", ex.Message);
        }

        [Test]

        public void AddFootNote_WorksProperly()
        {
            Assert.AreEqual(0, book.FootnoteCount);
            book.AddFootnote(16, "Note");

            Assert.AreEqual(1, book.FootnoteCount);
        }

        [Test]

        public void AddFootNote_ThrowsException_WhenKeyAlreadyThere()
        {
            book.AddFootnote(16, "Note");

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => book.AddFootnote(16, "Note"));

            Assert.AreEqual("Footnote already exists!", ex.Message);
        }

        [Test]

        public void FindNote_WorksProperly()
        {
            book.AddFootnote(16, "Note");
            book.AddFootnote(32, "Note2");

            Assert.AreEqual("Footnote #32: Note2", book.FindFootnote(32));
        }

        [Test]

        public void FindNote_ThrowsException_WhenNotDoesNotExist()
        {
            book.AddFootnote(16, "Note");
            book.AddFootnote(32, "Note2");

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => book.FindFootnote(1));

            Assert.AreEqual("Footnote doesn't exists!", ex.Message);
        }

        [Test]

        public void AlterNote_WorksProperly()
        {
            book.AddFootnote(16, "Note");
            book.AddFootnote(32, "Note2");

            Assert.AreEqual("Footnote #32: Note2", book.FindFootnote(32));
            book.AlterFootnote(32, "Altered");
            Assert.AreEqual("Footnote #32: Altered", book.FindFootnote(32));

        }

        [Test]

        public void AlterNote_ThrowsException_WhenNotDoesNotExist()
        {
            book.AddFootnote(16, "Note");
            book.AddFootnote(32, "Note2");

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => book.AlterFootnote(1, "NEW"));

            Assert.AreEqual("Footnote does not exists!", ex.Message);
        }
    }
}