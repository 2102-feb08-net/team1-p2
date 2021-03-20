using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using LooseLeaf.DataAccess;
using LooseLeaf.Business.Models;
using LooseLeaf.Business.IRepositories;
using LooseLeaf.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using LooseLeaf.Business;
using System.Net.Http;

namespace LooseLeaf.Tests.IntegrationTests
{
    public class OwnedBookRepositoryTests
    {
        [Fact]
        public async Task OwnedBookRepository_AddOwnedBook()
        {
            // arrange
            const int userId = 1;
            const string username = "user";
            const string title = "The Martian";
            const string author = "Beyonce";
            long isbn = 9780804139038;

            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                await contextFactory.CreateUser(arrangeContext, username);
                isbn = await contextFactory.CreateBook(arrangeContext, title, author);
                await arrangeContext.SaveChangesAsync();
            }

            Mock<IIsbnData> mockIsbn = new Mock<IIsbnData>();
            mockIsbn.Setup(x => x.IsbnValue).Returns(isbn);

            Mock<IBook> fakeBook = new Mock<IBook>();
            fakeBook.Setup(x => x.Title).Returns(title);
            fakeBook.Setup(x => x.Author).Returns(author);
            fakeBook.Setup(x => x.Genres).Returns(new List<string>() { "Test" });
            fakeBook.Setup(x => x.Isbn).Returns(isbn);

            Mock<IOwnedBook> fakeOwnedBook = new Mock<IOwnedBook>();
            fakeOwnedBook.Setup(x => x.Availability).Returns(Availability.Available);
            fakeOwnedBook.Setup(x => x.Condition).Returns(PhysicalCondition.LikeNew);
            fakeOwnedBook.Setup(x => x.OwnerId).Returns(userId);
            fakeOwnedBook.Setup(x => x.Isbn).Returns(mockIsbn.Object);
            GoogleBooks googleBooks = new GoogleBooks(new HttpClient(), null);

            // act
            using (LooseLeafContext actContext = contextFactory.CreateContext())
            {
                IOwnedBookRepository ownedBookRepo = new OwnedBookRepository(actContext);

                await ownedBookRepo.AddOwnedBookAsync(fakeOwnedBook.Object, googleBooks);
                await actContext.SaveChangesAsync();
            }

            // assert
            using LooseLeafContext assertContext = contextFactory.CreateContext();
            var ownedBook = await assertContext.OwnedBooks.Include(x => x.User).Include(x => x.Book).SingleAsync();

            Assert.Equal(isbn, ownedBook.Book.Isbn);
            Assert.Equal(username, ownedBook.User.Username);
        }

        [Fact]
        public async Task OwnedBookRepository_UpdateStatusOfOwnedBook()
        {
            // arrange
            const int ownedBookId = 1;
            const int userId = 1;
            const int bookId = 1;
            Availability availability = Availability.CheckedOut;
            PhysicalCondition condition = PhysicalCondition.Fair;

            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                await contextFactory.CreateOwnedBook(arrangeContext, userId, bookId);
                await arrangeContext.SaveChangesAsync();
            }

            // act
            using (LooseLeafContext actContext = contextFactory.CreateContext())
            {
                IOwnedBookRepository ownedBookRepo = new OwnedBookRepository(actContext);

                await ownedBookRepo.UpdateOwnedBookStatus(ownedBookId, availability, condition);
                await actContext.SaveChangesAsync();
            }

            // assert
            using LooseLeafContext assertContext = contextFactory.CreateContext();
            var ownedBook = await assertContext.OwnedBooks.Include(x => x.User).Include(x => x.Book).SingleAsync();

            Assert.Equal(userId, ownedBook.UserId);
            Assert.Equal(bookId, ownedBook.BookId);
            Assert.Equal((int)condition, ownedBook.ConditionId);
            Assert.Equal((int)availability, ownedBook.AvailabilityStatusId);
        }

        [Fact]
        public async Task OwnedBookRepository_GetOwnedBooks()
        {
            // arrange
            const int userId = 1;
            const int bookId = 1;
            const int bookId2 = 2;

            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                await contextFactory.CreateBook(arrangeContext);
                await contextFactory.CreateBook(arrangeContext);
                await contextFactory.CreateOwnedBook(arrangeContext, userId, bookId);
                await contextFactory.CreateOwnedBook(arrangeContext, userId, bookId2);
                await arrangeContext.SaveChangesAsync();
            }

            // act
            IEnumerable<IOwnedBook> ownedBooks;
            using (LooseLeafContext actContext = contextFactory.CreateContext())
            {
                IOwnedBookRepository ownedBookRepo = new OwnedBookRepository(actContext);

                ownedBooks = await ownedBookRepo.GetOwnedBooksAsync(new OwnedBookSearchParams());
            }

            // assert
            Assert.Equal(2, ownedBooks.Count());
        }
    }
}