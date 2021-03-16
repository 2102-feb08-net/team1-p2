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
        private int genreId = 2;

        [Fact]
        public void Book_Constructor_Pass()
        {
            // arrange
            string title = "My Book";
            string author = "Test Author";
            string isbn = "9781234567890";

            // act
            IBook book = new Book(title, author, isbn, genreId);

            // assert
            Assert.NotNull(book);
        }

        [Fact]
        public void Book_GetTitle_Pass()
        {
            // arrange
            string title = "My Book";
            string author = "Test Author";
            string isbn = "9781234567890";

            // act
            IBook book = new Book(title, author, isbn, genreId);

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

            // act
            IBook book = new Book(title, author, isbn, genreId);

            // assert
            Assert.Equal(author, book.Author);
        }

        [Fact]
        public void Book_GetGenreId_Pass()
        {
            // arrange
            string title = "My Book";
            string author = "Test Author";
            string isbn = "9781234567890";

            // act
            IBook book = new Book(title, author, isbn, genreId);

            // assert
            Assert.Equal(genreId, book.GenreId);
        }

        [Fact]
        public void Book_NullISBN_Exception()
        {
            // arrange
            const string title = "My Book";
            const string author = "Test Author";
            const string isbn = null;

            // act
            IBook constructBook() => new Book(title, author, isbn, genreId);

            // assert
            Assert.Throws<ArgumentNullException>(constructBook);
        }

        [Theory]
        [InlineData("Hello 9781234567890")]
        [InlineData("97812345678901")]
        [InlineData("978123456789")]
        [InlineData("234567890")]
        public void Book_InvalidISBN_Exception(string isbn)
        {
            // arrange
            const string title = "My Book";
            const string author = "Test Author";

            // act
            IBook constructBook() => new Book(title, author, isbn, genreId);

            // assert
            Assert.Throws<ArgumentException>(constructBook);
        }

        [Theory]
        [InlineData("1-23-456789-0", 1234567890)]
        [InlineData("978-1-23-456789-0", 9781234567890)]
        public void Book_FormatISBN_Pass(string isbn, ulong expectedIsbn)
        {
            // arrange
            const string title = "My Book";
            const string author = "Test Author";

            // act
            IBook book = new Book(title, author, isbn, genreId);

            // assert
            Assert.Equal(expectedIsbn, book.Isbn);
        }

        [Fact]
        public void Book_NullTitle_Exception()
        {
            // arrange
            const string title = null;
            const string author = "Test Author";
            const string isbn = "9781234567890";

            // act
            IBook constructBook() => new Book(title, author, isbn, genreId);

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
            IBook constructBook() => new Book(title, author, isbn, genreId);

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
            IBook constructBook() => new Book(title, author, isbn, genreId);

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
            IBook constructBook() => new Book(title, author, isbn, genreId);

            // assert
            Assert.Throws<ArgumentException>(constructBook);
        }
    }
}