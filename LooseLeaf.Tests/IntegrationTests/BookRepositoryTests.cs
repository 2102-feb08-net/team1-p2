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
            const string title = "Gone Vith the Vend";
            const string author = "Vladimir";
            const long isbn = 9781234567890;

            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                await contextFactory.CreateGenre(arrangeContext);
                await arrangeContext.SaveChangesAsync();
            }

            Mock<IBook> fakeBook = new Mock<IBook>();
            fakeBook.Setup(x => x.Title).Returns(title);
            fakeBook.Setup(x => x.Author).Returns(author);
            fakeBook.Setup(x => x.GenreId).Returns(1);
            fakeBook.Setup(x => x.Isbn).Returns(isbn);

            // act
            using (LooseLeafContext actContext = contextFactory.CreateContext())
            {
                IBookRepository bookRepo = new BookRepository(actContext);

                await bookRepo.AddBook(fakeBook.Object);
                await actContext.SaveChangesAsync();
            }

            // assert
            using (LooseLeafContext assertContext = contextFactory.CreateContext())
            {
                var book = await assertContext.Books.Include(x => x.Genre).SingleAsync();

                Assert.Equal(title, book.Title);
                Assert.Equal(author, book.Author);
                Assert.Equal(isbn, book.Isbn);
            }
        }


        [Fact]
        public async Task GetSpecificBooks_ReturnList()
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
                await contextFactory.CreateGenre(arrangeContext);
                await arrangeContext.SaveChangesAsync();

                List<DataAccess.Book> books = new List<DataAccess.Book>()
                    {
                        new DataAccess.Book() { Title = title1, Author = author1, Isbn = isbn1, GenreId = 1 },
                        new DataAccess.Book() { Title = title2, Author = author2, Isbn = isbn2, GenreId = 1 }
                    };

                await arrangeContext.AddRangeAsync(books);
                await arrangeContext.SaveChangesAsync();

            }

            using (LooseLeafContext actContext = contextFactory.CreateContext())
            {
                await contextFactory.CreateGenre(actContext);
                await actContext.SaveChangesAsync();

                IBookRepository bookRepo = new BookRepository(actContext);
                await bookRepo.GetSpecificBooks(new BookSearchParams());
                await actContext.SaveChangesAsync();
            };

            // assert
            using (LooseLeafContext assertContext = contextFactory.CreateContext())
            {
                var book = await assertContext.Books.Include(x => x.Genre).ToListAsync();
                Assert.Equal(2, book.Count);
            }
        }
    }
}