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
            using var contextFactory = new TestLooseLeafContextFactory();
            using LooseLeafContext context = contextFactory.CreateContext();
            var insertedBook = new Book
            {
                Title = "Gone with the Wind",
                Author = "JoJo",
                Isbn = "12345678925814",
                Genreid = 15
            };
            context.Books.Add(insertedBook);
            context.SaveChanges();
            var repo = new BookRepository(context);

            // act
            Business.Models.Book book = repo.Get(insertedBook.Id);

            // assert
            Assert.Equal(insertedBook.Id, book.Id);
            Assert.Equal(insertedBook.Title, book.Title);
            Assert.Equal(insertedBook.Author, book.Author);
            Assert.Equal(insertedBook.Isbn, book.Isbn);
            Assert.Equal(insertedBook.Genreid, book.Genreid);
        }
    }
}
