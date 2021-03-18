using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using LooseLeaf.DataAccess;
using LooseLeaf.Business.IRepositories;
using LooseLeaf.DataAccess.Repositories;
using LooseLeaf.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace LooseLeaf.Tests.IntegrationTests
{
    public class WishlistRepositoryTests
    {
        [Fact]
        public async Task WishlistRepository_GetWishlist_ReturnList()
        {
            // arrange
            const string username = "user";
            const int userId = 1;
            using var contextFactory = new TestLooseLeafContextFactory();

            List<DataAccess.Wishlist> wishlists = new List<DataAccess.Wishlist>()
            {
                new DataAccess.Wishlist() { UserId = userId, BookId = 1 },
                new DataAccess.Wishlist() { UserId = userId, BookId = 2 },
                new DataAccess.Wishlist() { UserId = userId, BookId = 3 }
            };

            using (LooseLeafContext addContext = contextFactory.CreateContext())
            {
                await contextFactory.CreateUser(addContext, username);
                await contextFactory.CreateBook(addContext, "Book 1", "Author 1");
                await contextFactory.CreateBook(addContext, "Book 2", "Author 2");
                await contextFactory.CreateBook(addContext, "Book 3", "Author 3");
                await addContext.SaveChangesAsync();

                await addContext.Wishlists.AddRangeAsync(wishlists);
                await addContext.SaveChangesAsync();
            }

            using LooseLeafContext context = contextFactory.CreateContext();
            IWishlistRepository wishlistRepository = new WishlistRepository(context);

            // act
            var books = await wishlistRepository.GetUserWishlist(userId);

            // assert
            Assert.Equal(wishlists.Count, books.Count());
        }

        [Fact]
        public async Task WishlistRepository_AddBookToUserWishlist()
        {
            // arrange

            //Constants to compare user and book with test results.
            const string username = "user";
            const string bookName = "Test Book";
            const string authorName = "The Author";
            long isbn;
            const int userId = 1;
            const int bookId = 1;

            using var contextFactory = new TestLooseLeafContextFactory();

            // Add in foreign key / required database data. Wishlist requires a user and a book type to exist in the database.
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                await contextFactory.CreateUser(arrangeContext, username);
                isbn = await contextFactory.CreateBook(arrangeContext, bookName, authorName);
                await arrangeContext.SaveChangesAsync();
            }

            // act
            using (LooseLeafContext actContext = contextFactory.CreateContext())
            {
                // Create Repository
                IWishlistRepository wishlistRepository = new WishlistRepository(actContext);

                // Test repository method
                await wishlistRepository.AddBookToUserWishlist(userId, bookId);
                await actContext.SaveChangesAsync();
            }

            // assert
            // Create a new context to ensure that data was saved to database.
            using (LooseLeafContext assertContext = contextFactory.CreateContext())
            {
                var wishlist = await assertContext.Wishlists.Include(w => w.User).Include(w => w.Book).SingleAsync();

                // assert that the book and user are the same as one added to the wishlist.
                Assert.Equal(username, wishlist.User.Username);
                Assert.Equal(bookName, wishlist.Book.Title);
                Assert.Equal(authorName, wishlist.Book.Author);
            }
        }

        [Fact]
        public async Task WishlistRepository_RemoveBookFromUserWishlist()
        {
            // arrange
            const string username = "user";
            const string bookName = "Test Book";
            const string authorName = "The Author";
            long isbn;
            int originalWishlistCount;
            const int userId = 1;
            const int bookId = 1;

            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                await contextFactory.CreateUser(arrangeContext, username);
                isbn = await contextFactory.CreateBook(arrangeContext, bookName, authorName);
                await arrangeContext.Wishlists.AddAsync(new DataAccess.Wishlist() { UserId = 1, BookId = 1 });
                await arrangeContext.SaveChangesAsync();
            }

            // act
            using (LooseLeafContext actContext = contextFactory.CreateContext())
            {
                IWishlistRepository wishlistRepository = new WishlistRepository(actContext);
                originalWishlistCount = actContext.Wishlists.Count();
                await wishlistRepository.RemoveBookFromUserWishlist(userId, bookId);
                await actContext.SaveChangesAsync();
            }

            // assert
            using (LooseLeafContext assertContext = contextFactory.CreateContext())
            {
                Assert.Equal(0, assertContext.Wishlists.Count());
                Assert.Equal(1, originalWishlistCount);
            }
        }
    }
}