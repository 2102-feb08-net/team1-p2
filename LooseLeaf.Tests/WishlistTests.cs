using LooseLeaf.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LooseLeaf.Tests
{
    public class WishlistTests
    {
        [Fact]
        public void Wishlist_Construct_Pass()
        {
            // arrange

            // act
            IWishlist wishlist = new Wishlist();

            // assert
            Assert.NotNull(wishlist);
        }

        [Fact]
        public void Wishlist_GetBooks()
        {
            // arrange
            IWishlist wishlist = new Wishlist();

            // act
            var books = wishlist.Books;

            // assert
            Assert.NotNull(books);
        }
    }
}