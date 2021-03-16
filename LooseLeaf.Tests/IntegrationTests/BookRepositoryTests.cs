using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LooseLeaf.DataAccess;
using LooseLeaf.DataAccess.Repositories;
using Moq;
using Xunit;

namespace LooseLeaf.Tests.IntegrationTests
{
    public class BookRepositoryTests
    {
        [Fact]
        public async Task GetBookById_Returns_Book()
        {
            // arrange- set up DbContext and DbSet mock

            var insertedBook = new Book
            {
                Title = "Gone with the Wind",
                Author = "JoJo",
                Isbn = 1234567892581,
                GenreId = 1
            };

            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                arrangeContext.Genres.Add(new Genre() { GenreName = "Test Genre" });
                arrangeContext.SaveChanges();

                arrangeContext.Books.Add(insertedBook);
                arrangeContext.SaveChanges();
            }

            using var context = contextFactory.CreateContext();
            var repo = new BookRepository(context);

            // act
            Business.Models.IBook book = await repo.GetBook(insertedBook.Id);

            // assert
            Assert.Equal(insertedBook.Id, book.Id);
            Assert.Equal(insertedBook.Title, book.Title);
            Assert.Equal(insertedBook.Author, book.Author);
            Assert.Equal(insertedBook.Isbn, book.Isbn);
            Assert.Equal(insertedBook.GenreId, book.GenreId);
        }
    }
}