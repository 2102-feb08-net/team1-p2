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
            };

            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                arrangeContext.Genres.Add(new Genre() { GenreName = "Test Genre", Book = insertedBook });
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
            Assert.Single(book.Genres);
        }

        [Fact]
        public async Task GetAllBooks_ReturnList()
        {
            // arrange
            const string title1 = "Brave Little Toaster";
            const string author1 = "Lil Wayne";
            long isbn1 = 1234567890123;
            const string title2 = "Three Blind Mice";
            const string author2 = "Celine Dion";
            long isbn2 = 1234567890124;

            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                List<DataAccess.Book> books = new List<DataAccess.Book>()
                {
                    new DataAccess.Book() { Title = title1, Author = author1, Isbn = isbn1},
                    new DataAccess.Book() { Title = title2, Author = author2, Isbn = isbn2}
                };

                await contextFactory.CreateGenre(arrangeContext, books[0]);
                await contextFactory.CreateGenre(arrangeContext, books[1]);
                await arrangeContext.AddRangeAsync(books);
                await arrangeContext.SaveChangesAsync();
            }

            using (LooseLeafContext actContext = contextFactory.CreateContext())
            {
                IBookRepository bookRepo = new BookRepository(actContext);
                await bookRepo.GetAllBooks(new BookSearchParams());
                await actContext.SaveChangesAsync();
            };

            // assert
            using (LooseLeafContext assertContext = contextFactory.CreateContext())
            {
                var book = await assertContext.Books.ToListAsync();
                Assert.Equal(2, book.Count);
            }
        }
    }
}