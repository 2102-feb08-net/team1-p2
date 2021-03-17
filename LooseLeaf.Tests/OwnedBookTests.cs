using LooseLeaf.Business.Models;
using Moq;
using System;
using Xunit;

namespace LooseLeaf.Tests
{
    public class OwnedBookTests
    {
        private int id = 1;

        [Fact]
        public void OwnedBook_Construct_Pass()
        {
            // arrange
            IBook book = new Mock<IBook>().Object;
            IUser user = new Mock<IUser>().Object;
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook ownedBook = new OwnedBook(id, book, user, condition, availability);

            // assert
            Assert.NotNull(ownedBook);
        }

        [Fact]
        public void OwnedBook_NullBook_Fail()
        {
            // arrange
            IUser user = new Mock<IUser>().Object;
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook constructOwnedBook() => new OwnedBook(id, null, user, condition, availability);

            // assert
            Assert.Throws<ArgumentNullException>(constructOwnedBook);
        }

        [Fact]
        public void OwnedBook_NullUser_Fail()
        {
            // arrange
            IBook book = new Mock<IBook>().Object;
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook constructOwnedBook() => new OwnedBook(id, book, null, condition, availability);

            // assert
            Assert.Throws<ArgumentNullException>(constructOwnedBook);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10)]
        public void OwnedBook_InvalidPhysicalCondition_Fail(int condition)
        {
            // arrange
            IBook book = new Mock<IBook>().Object;
            IUser user = new Mock<IUser>().Object;
            Availability availability = Availability.Available;

            // act
            IOwnedBook constructOwnedBook() => new OwnedBook(id, book, user, (PhysicalCondition)condition, availability);

            // assert
            Assert.Throws<ArgumentException>(constructOwnedBook);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10)]
        public void OwnedBook_InvalidAvailability_Fail(int availability)
        {
            // arrange
            IBook book = new Mock<IBook>().Object;
            IUser user = new Mock<IUser>().Object;
            PhysicalCondition condition = PhysicalCondition.LikeNew;

            // act
            IOwnedBook constructOwnedBook() => new OwnedBook(id, book, user, condition, (Availability)availability);

            // assert
            Assert.Throws<ArgumentException>(constructOwnedBook);
        }

        [Fact]
        public void OwnedBook_GetBook()
        {
            // arrange
            IBook book = new Mock<IBook>().Object;
            IUser user = new Mock<IUser>().Object;
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook ownedBook = new OwnedBook(id, book, user, condition, availability);

            // assert
            Assert.Equal(book, ownedBook.Book);
        }

        [Fact]
        public void OwnedBook_GetUser()
        {
            // arrange
            IBook book = new Mock<IBook>().Object;
            IUser user = new Mock<IUser>().Object;
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook ownedBook = new OwnedBook(id, book, user, condition, availability);

            // assert
            Assert.Equal(user, ownedBook.Owner);
        }

        [Fact]
        public void OwnedBook_GetCondition()
        {
            // arrange
            IBook book = new Mock<IBook>().Object;
            IUser user = new Mock<IUser>().Object;
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook ownedBook = new OwnedBook(id, book, user, condition, availability);

            // assert
            Assert.Equal(condition, ownedBook.Condition);
        }

        [Fact]
        public void OwnedBook_GetAvailabilty()
        {
            // arrange
            IBook book = new Mock<IBook>().Object;
            IUser user = new Mock<IUser>().Object;
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook ownedBook = new OwnedBook(id, book, user, condition, availability);

            // assert
            Assert.Equal(availability, ownedBook.Availability);
        }

        [Fact]
        public void OwnedBook_GetId()
        {
            // arrange
            IBook book = new Mock<IBook>().Object;
            IUser user = new Mock<IUser>().Object;
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook ownedBook = new OwnedBook(id, book, user, condition, availability);

            // assert
            Assert.Equal(id, ownedBook.Id);
        }
    }
}