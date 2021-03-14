﻿using LooseLeaf.Business.Models;
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
            var fakeAddress = new Mock<IAddress>();

            // act
            IUser user = new User(userName, email, fakeAddress.Object);

            // assert
            Assert.NotNull(user);
        }

        [Fact]
        public void User_NullUserName_Fail()
        {
            // arrange
            const string userName = null;
            const string email = "somebody@website.com";
            var fakeAddress = new Mock<IAddress>();

            // act
            Func<IUser> buildUser = () => new User(userName, email, fakeAddress.Object);

            // assert
            Assert.Throws<ArgumentNullException>(buildUser);
        }

        [Fact]
        public void User_NullEmail_Fail()
        {
            // arrange
            const string userName = "firstUser";
            const string email = null;
            var fakeAddress = new Mock<IAddress>();

            // act
            Func<IUser> buildUser = () => new User(userName, email, fakeAddress.Object);

            // assert
            Assert.Throws<ArgumentNullException>(buildUser);
        }

        [Fact]
        public void User_InvalidEmail_Fail()
        {
            // arrange
            const string userName = "firstUser";
            const string email = "somebodywebsitecom";
            var fakeAddress = new Mock<IAddress>();

            // act
            Func<IUser> buildUser = () => new User(userName, email, fakeAddress.Object);

            // assert
            Assert.Throws<FormatException>(buildUser);
        }
    }
}