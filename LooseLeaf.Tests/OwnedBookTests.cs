using LooseLeaf.Business.Models;
using Moq;
using System;
using Xunit;

namespace LooseLeaf.Tests
{
    public class OwnedBookTests
    {
        private readonly int id = 1;
        private readonly int ownerId = 1;
        private readonly IIsbnData isbn = new Mock<IIsbnData>().Object;

        [Fact]
        public void OwnedBook_Construct_Pass()
        {
            // arrange

            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook ownedBook = new OwnedBook(id, isbn, ownerId, condition, availability);

            // assert
            Assert.NotNull(ownedBook);
        }

        [Fact]
        public void OwnedBook_NullIsbn_Fail()
        {
            // arrange
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook constructOwnedBook() => new OwnedBook(id, null, ownerId, condition, availability);

            // assert
            Assert.Throws<ArgumentNullException>(constructOwnedBook);
        }

        [Fact]
        public void OwnedBook_DefaultUser_Fail()
        {
            // arrange
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook constructOwnedBook() => new OwnedBook(id, isbn, default, condition, availability);

            // assert
            Assert.Throws<ArgumentException>(constructOwnedBook);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10)]
        public void OwnedBook_InvalidPhysicalCondition_Fail(int condition)
        {
            // arrange
            Availability availability = Availability.Available;

            // act
            IOwnedBook constructOwnedBook() => new OwnedBook(id, isbn, ownerId, (PhysicalCondition)condition, availability);

            // assert
            Assert.Throws<ArgumentException>(constructOwnedBook);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10)]
        public void OwnedBook_InvalidAvailability_Fail(int availability)
        {
            // arrange
            PhysicalCondition condition = PhysicalCondition.LikeNew;

            // act
            IOwnedBook constructOwnedBook() => new OwnedBook(id, isbn, ownerId, condition, (Availability)availability);

            // assert
            Assert.Throws<ArgumentException>(constructOwnedBook);
        }

        [Fact]
        public void OwnedBook_GetBook()
        {
            // arrange
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook ownedBook = new OwnedBook(id, isbn, ownerId, condition, availability);

            // assert
            Assert.Equal(isbn, ownedBook.Isbn);
        }

        [Fact]
        public void OwnedBook_GetUser()
        {
            // arrange
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook ownedBook = new OwnedBook(id, isbn, ownerId, condition, availability);

            // assert
            Assert.Equal(ownerId, ownedBook.OwnerId);
        }

        [Fact]
        public void OwnedBook_GetCondition()
        {
            // arrange
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook ownedBook = new OwnedBook(id, isbn, ownerId, condition, availability);

            // assert
            Assert.Equal(condition, ownedBook.Condition);
        }

        [Fact]
        public void OwnedBook_GetAvailabilty()
        {
            // arrange
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook ownedBook = new OwnedBook(id, isbn, ownerId, condition, availability);

            // assert
            Assert.Equal(availability, ownedBook.Availability);
        }

        [Fact]
        public void OwnedBook_GetId()
        {
            // arrange
            PhysicalCondition condition = PhysicalCondition.LikeNew;
            Availability availability = Availability.Available;

            // act
            IOwnedBook ownedBook = new OwnedBook(id, isbn, ownerId, condition, availability);

            // assert
            Assert.Equal(id, ownedBook.Id);
        }
    }
}