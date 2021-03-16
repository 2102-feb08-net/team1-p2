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

namespace LooseLeaf.Tests.IntegrationTests
{
    public class WishlistRepositoryTests
    {
        [Fact]
        public async Task WishlistRepository_GetWishlist_ReturnList()
        {
            // arrange
            const string username = "user";
            Mock<IUser> fakeUser = new Mock<IUser>();
            fakeUser.Setup(u => u.UserName).Returns(username);
            using var contextFactory = new TestLooseLeafContextFactory();

            List<DataAccess.Wishlist> wishlists = new List<DataAccess.Wishlist>()
            {
                new DataAccess.Wishlist() { UserId = 1, BookId = 1 },
                new DataAccess.Wishlist() { UserId = 1, BookId = 2 },
                new DataAccess.Wishlist() { UserId = 1, BookId = 3 }
            };

            using (LooseLeafContext addContext = contextFactory.CreateContext())
            {
                await addContext.Addresses.AddAsync(new DataAccess.Address() { Address1 = "Street 1", City = "City", State = "State", Zipcode = "123456" });
                await addContext.SaveChangesAsync();
                await addContext.Users.AddAsync(new DataAccess.User() { Username = username, Userpassword = "password", Email = "user@website.com", Addressid = 1 });
                await addContext.Genres.AddAsync(new DataAccess.Genre() { Genre1 = "Story" });
                await addContext.SaveChangesAsync();
                await addContext.Books.AddRangeAsync(new List<DataAccess.Book>{
                    new DataAccess.Book() { Title = "Book 1", Author = "Author 1", Isbn = "1234567890123", Genreid = 1},
                    new DataAccess.Book() { Title = "Book 2", Author = "Author 2", Isbn = "1235567890123", Genreid = 1},
                    new DataAccess.Book() { Title = "Book 3", Author = "Author 3", Isbn = "1234777890123", Genreid = 1}
                });
                await addContext.SaveChangesAsync();
                await addContext.Wishlists.AddRangeAsync(wishlists);
                await addContext.SaveChangesAsync();
            }

            using LooseLeafContext context = contextFactory.CreateContext();
            IWishlistRepository wishlistRepository = new WishlistRepository(context);

            // act
            var books = await wishlistRepository.GetUserWishlist(fakeUser.Object);

            // assert
            Assert.Equal(wishlists.Count, books.Count());
        }
    }
}