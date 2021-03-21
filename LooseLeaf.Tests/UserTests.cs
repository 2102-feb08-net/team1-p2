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
        private const int userId = 1;

        [Fact]
        public void User_Construct_Pass()
        {
            // arrange
            const string userName = "firstUser";
            const string email = "somebody@website.com";
            IAddress fakeAddress = new Mock<IAddress>().Object;

            // act
            IUser user = new User(userId, userName, email);

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

            // act
            IUser buildUser() => new User(userId, userName, email);

            // assert
            Assert.Throws<ArgumentNullException>(buildUser);
        }

        [Fact]
        public void User_DefaultId_Fail()
        {
            // arrange
            const string userName = "firstUser";
            const string email = "somebody@website.com";
            IAddress fakeAddress = new Mock<IAddress>().Object;

            // act
            IUser buildUser() => new User(default, userName, email);

            // assert
            Assert.Throws<ArgumentException>(buildUser);
        }

        [Fact]
        public void User_NullEmail_Fail()
        {
            // arrange
            const string userName = "firstUser";
            const string email = null;
            IAddress fakeAddress = new Mock<IAddress>().Object;

            // act
            IUser buildUser() => new User(userId, userName, email);

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

            // act
            IUser buildUser() => new User(userId, userName, email);

            // assert
            Assert.Throws<FormatException>(buildUser);
        }

        [Fact]
        public void User_GetUserName()
        {
            // arrange
            const string userName = "firstUser";
            const string email = "somebody@website.com";
            IAddress fakeAddress = new Mock<IAddress>().Object;

            // act
            IUser user = new User(userId, userName, email);

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

            // act
            IUser user = new User(userId, userName, email);

            // assert
            Assert.Equal(email, user.Email);
        }

        [Fact]
        public void User_GetId()
        {
            // arrange
            const string userName = "firstUser";
            const string email = "somebody@website.com";
            IAddress fakeAddress = new Mock<IAddress>().Object;

            // act
            IUser user = new User(userId, userName, email);

            // assert
            Assert.Equal(userId, user.Id);
        }
    }
}