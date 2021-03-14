using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LooseLeaf.Business.Models;

namespace LooseLeaf.Tests
{
    public class BookTests
    {
        [Fact]
        public void Book_Constructor_Pass()
        {
            // arrange
            string title = "My Book";
            string author = "Test Author";
            string isbn = "9781234567890";
            DateTime datePublished = new DateTime(2000, 12, 31);

            // act
            IBook book = new Book(title, author, isbn, datePublished);

            // assert
            Assert.NotNull(book);
            Assert.Equal(title, book.Title);
            Assert.Equal(author, book.Author);
            Assert.Equal(isbn, book.ISBN.ToString());
            Assert.Equal(datePublished, book.PublishedDate);
        }

        [Fact]
        public void Book_GetTitle_Pass()
        {
            // arrange
            string title = "My Book";
            string author = "Test Author";
            string isbn = "9781234567890";
            DateTime datePublished = new DateTime(2000, 12, 31);

            // act
            IBook book = new Book(title, author, isbn, datePublished);

            // assert
            Assert.Equal(title, book.Title);
        }

        [Fact]
        public void Book_GetAuthor_Pass()
        {
            // arrange
            string title = "My Book";
            string author = "Test Author";
            string isbn = "9781234567890";
            DateTime datePublished = new DateTime(2000, 12, 31);

            // act
            IBook book = new Book(title, author, isbn, datePublished);

            // assert
            Assert.Equal(author, book.Author);
        }

        [Fact]
        public void Book_GetDatePublished_Pass()
        {
            // arrange
            string title = "My Book";
            string author = "Test Author";
            string isbn = "9781234567890";
            DateTime datePublished = new DateTime(2000, 12, 31);

            // act
            IBook book = new Book(title, author, isbn, datePublished);

            // assert
            Assert.Equal(datePublished, book.PublishedDate);
        }

        [Fact]
        public void Book_NullISBN_Exception()
        {
            // arrange
            const string title = "My Book";
            const string author = "Test Author";
            const string isbn = null;

            // act
            static IBook constructBook() => new Book(title, author, isbn, new DateTime(2000, 12, 31));

            // assert
            Assert.Throws<ArgumentNullException>(constructBook);
        }

        [Fact]
        public void Book_InvalidISBN_Exception()
        {
            // arrange
            const string title = "My Book";
            const string author = "Test Author";
            const string isbn = "Hello 9781234567890";

            // act
            static IBook constructBook() => new Book(title, author, isbn, new DateTime(2000, 12, 31));

            // assert
            Assert.Throws<ArgumentException>(constructBook);
        }

        [Fact]
        public void Book_FormatISBN_Pass()
        {
            // arrange
            const string title = "My Book";
            const string author = "Test Author";
            const string isbn = "978-1-23-456789-0";
            const ulong expectedIsbn = 9781234567890;

            // act
            IBook book = new Book(title, author, isbn, new DateTime(2000, 12, 31));

            // assert
            Assert.Equal(expectedIsbn, book.ISBN);
        }

        [Fact]
        public void Book_NullTitle_Exception()
        {
            // arrange
            const string title = null;
            const string author = "Test Author";
            const string isbn = "9781234567890";

            // act
            static IBook constructBook() => new Book(title, author, isbn, new DateTime(2000, 12, 31));

            // assert
            Assert.Throws<ArgumentNullException>(constructBook);
        }

        [Fact]
        public void Book_EmptyTitle_Exception()
        {
            // arrange
            const string title = "   ";
            const string author = "Test Author";
            const string isbn = "9781234567890";

            // act
            static IBook constructBook() => new Book(title, author, isbn, new DateTime(2000, 12, 31));

            // assert
            Assert.Throws<ArgumentException>(constructBook);
        }

        [Fact]
        public void Book_NullAuthor_Exception()
        {
            // arrange
            const string title = "My Book";
            const string author = null;
            const string isbn = "9781234567890";

            // act
            static IBook constructBook() => new Book(title, author, isbn, new DateTime(2000, 12, 31));

            // assert
            Assert.Throws<ArgumentNullException>(constructBook);
        }

        [Fact]
        public void Book_EmptyAuthor_Exception()
        {
            // arrange
            const string title = "My Book";
            const string author = "    ";
            const string isbn = "9781234567890";

            // act
            static IBook constructBook() => new Book(title, author, isbn, new DateTime(2000, 12, 31));

            // assert
            Assert.Throws<ArgumentException>(constructBook);
        }
    }
}