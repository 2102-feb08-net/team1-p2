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

namespace LooseLeaf.Tests.IntegrationTests
{
    public class OwnedBookRepositoryTests
    {
        [Fact]
        public async Task OwnedBookRepository_AddOwnedBook()
        {
            // arrange
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

            Mock<IUser> fakeUser = new Mock<IUser>();
            fakeUser.Setup(u => u.UserName).Returns(username);

            Mock<IBook> fakeBook = new Mock<IBook>();
            fakeBook.Setup(x => x.Title).Returns(title);
            fakeBook.Setup(x => x.Author).Returns(author);
            fakeBook.Setup(x => x.Genres).Returns(new List<string>() { "Test" });
            fakeBook.Setup(x => x.Isbn).Returns(isbn);

            Mock<IOwnedBook> fakeOwnedBook = new Mock<IOwnedBook>();
            fakeOwnedBook.Setup(x => x.Availability).Returns(Availability.Available);
            fakeOwnedBook.Setup(x => x.Condition).Returns(PhysicalCondition.LikeNew);
            fakeOwnedBook.Setup(x => x.Owner).Returns(fakeUser.Object);
            fakeOwnedBook.Setup(x => x.Book).Returns(fakeBook.Object);

            // act
            using (LooseLeafContext actContext = contextFactory.CreateContext())
            {
                IOwnedBookRepository ownedBookRepo = new OwnedBookRepository(actContext);

                await ownedBookRepo.AddOwnedBookAsync(fakeOwnedBook.Object);
                await actContext.SaveChangesAsync();
            }

            // assert
            using (LooseLeafContext assertContext = contextFactory.CreateContext())
            {
                var ownedBook = await assertContext.OwnedBooks.Include(x => x.User).Include(x => x.Book).SingleAsync();

                Assert.Equal(isbn, ownedBook.Book.Isbn);
                Assert.Equal(username, ownedBook.User.Username);
            }
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
            using (LooseLeafContext assertContext = contextFactory.CreateContext())
            {
                var ownedBook = await assertContext.OwnedBooks.Include(x => x.User).Include(x => x.Book).SingleAsync();

                Assert.Equal(userId, ownedBook.UserId);
                Assert.Equal(bookId, ownedBook.BookId);
                Assert.Equal((int)condition, ownedBook.ConditionId);
                Assert.Equal((int)availability, ownedBook.AvailabilityStatusId);
            }
        }
    }
}