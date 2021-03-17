using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LooseLeaf.DataAccess;
using LooseLeaf.DataAccess.Repositories;
using LooseLeaf.Business.Models;
using LooseLeaf.Business.IRepositories;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace LooseLeaf.Tests.IntegrationTests
{
    public class BookRepositoryTests
    {
        [Fact]
        public async Task GetBookById_Returns_Book()
        {
            // arrange
            var insertedBook = new DataAccess.Book
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
            IBook book = await repo.GetBook(insertedBook.Id);

            // assert
            Assert.Equal(insertedBook.Id, book.Id);
            Assert.Equal(insertedBook.Title, book.Title);
            Assert.Equal(insertedBook.Author, book.Author);
            Assert.Equal(insertedBook.Isbn, book.Isbn);
            Assert.Equal(insertedBook.GenreId, book.GenreId);
        }


        [Fact]
        public async Task AddBook()
        {
            // arrange
            const string title = "Gone with the Vend";
            const string author = "Beyonce";
            long isbn;

            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                isbn = await contextFactory.CreateBook(arrangeContext, title, author);              
                await arrangeContext.SaveChangesAsync();
            }

            Mock<IBook> fakeBook = new Mock<IBook>();
            fakeBook.Setup(x => x.Title).Returns(title);
            fakeBook.Setup(x => x.Author).Returns(author);
            fakeBook.Setup(x => x.Isbn).Returns(isbn);

            using (LooseLeafContext actContext = contextFactory.CreateContext())
            {
                IBookRepository bookRepo = new BookRepository(actContext);

                await bookRepo.AddBook(fakeBook.Object);  // don't know what's wrong here
                await actContext.SaveChangesAsync();
            }

            using (LooseLeafContext assertContext = contextFactory.CreateContext())
            {
                var book = await assertContext.Books.Include(x => x.Genre).SingleAsync();

                Assert.Equal(title, book.Title);
                Assert.Equal(author, book.Author);
            }
        }
    }
}