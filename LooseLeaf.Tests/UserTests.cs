using LooseLeaf.Business.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LooseLeaf.Tests
{
    public class UserTests
    {
        [Fact]
        public void User_Construct_Pass()
        {
            // arrange
            const string userName = "firstUser";
            const string email = "somebody@website.com";
            IAddress fakeAddress = new Mock<IAddress>().Object;
            IWishlist fakeWishlist = new Mock<IWishlist>().Object;

            // act
            IUser user = new User(userName, email, fakeAddress, fakeWishlist);

            // assert
            Assert.NotNull(user);
        }

        [Fact]
        public void User_NullUserName_Fail()
        {
            // arrange
            const string userName = null;
            const string email = "somebody@website.com";
            IAddress fakeAddress = new Mock<IAddress>().Object;
            IWishlist fakeWishlist = new Mock<IWishlist>().Object;

            // act
            IUser buildUser() => new User(userName, email, fakeAddress, fakeWishlist);

            // assert
            Assert.Throws<ArgumentNullException>(buildUser);
        }

        [Fact]
        public void User_NullEmail_Fail()
        {
            // arrange
            const string userName = "firstUser";
            const string email = null;
            IAddress fakeAddress = new Mock<IAddress>().Object;
            IWishlist fakeWishlist = new Mock<IWishlist>().Object;

            // act
            IUser buildUser() => new User(userName, email, fakeAddress, fakeWishlist);

            // assert
            Assert.Throws<ArgumentNullException>(buildUser);
        }

        [Fact]
        public void User_InvalidEmail_Fail()
        {
            // arrange
            const string userName = "firstUser";
            const string email = "somebodywebsitecom";
            IAddress fakeAddress = new Mock<IAddress>().Object;
            IWishlist fakeWishlist = new Mock<IWishlist>().Object;

            // act
            IUser buildUser() => new User(userName, email, fakeAddress, fakeWishlist);

            // assert
            Assert.Throws<FormatException>(buildUser);
        }

        [Fact]
        public void User_NullAddress_Fail()
        {
            // arrange
            const string userName = "firstUser";
            const string email = "somebody@website.com";
            IAddress fakeAddress = null;
            IWishlist fakeWishlist = new Mock<IWishlist>().Object;

            // act
            IUser buildUser() => new User(userName, email, fakeAddress, fakeWishlist);

            // assert
            Assert.Throws<ArgumentNullException>(buildUser);
        }

        [Fact]
        public void User_NullWishlist_Fail()
        {
            // arrange
            const string userName = "firstUser";
            const string email = "somebody@website.com";
            IAddress fakeAddress = new Mock<IAddress>().Object;
            IWishlist fakeWishlist = null;

            // act
            IUser buildUser() => new User(userName, email, fakeAddress, fakeWishlist);

            // assert
            Assert.Throws<ArgumentNullException>(buildUser);
        }

        [Fact]
        public void User_GetUserName()
        {
            // arrange
            const string userName = "firstUser";
            const string email = "somebody@website.com";
            IAddress fakeAddress = new Mock<IAddress>().Object;
            IWishlist fakeWishlist = new Mock<IWishlist>().Object;

            // act
            IUser user = new User(userName, email, fakeAddress, fakeWishlist);

            // assert
            Assert.Equal(userName, user.UserName);
        }

        [Fact]
        public void User_GetEmail()
        {
            // arrange
            const string userName = "firstUser";
            const string email = "somebody@website.com";
            IAddress fakeAddress = new Mock<IAddress>().Object;
            IWishlist fakeWishlist = new Mock<IWishlist>().Object;

            // act
            IUser user = new User(userName, email, fakeAddress, fakeWishlist);

            // assert
            Assert.Equal(email, user.Email);
        }

        [Fact]
        public void User_GetAddress()
        {
            // arrange
            const string userName = "firstUser";
            const string email = "somebody@website.com";
            IAddress fakeAddress = new Mock<IAddress>().Object;
            IWishlist fakeWishlist = new Mock<IWishlist>().Object;

            // act
            IUser user = new User(userName, email, fakeAddress, fakeWishlist);

            // assert
            Assert.Equal(fakeAddress, user.Address);
        }

        [Fact]
        public void User_GetWishlist()
        {
            // arrange
            const string userName = "firstUser";
            const string email = "somebody@website.com";
            IAddress fakeAddress = new Mock<IAddress>().Object;
            IWishlist fakeWishlist = new Mock<IWishlist>().Object;

            // act
            IUser user = new User(userName, email, fakeAddress, fakeWishlist);

            // assert
            Assert.Equal(fakeWishlist, user.Wishlist);
        }
    }
}