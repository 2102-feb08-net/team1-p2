using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LooseLeaf.Business.Models;
using Moq;

namespace LooseLeaf.Tests
{
    public class LoanTests
    {
        private const int fakeLender = 1;
        private const int fakeBorrower = 2;
        private string message = "I want to borrow these books.";
        private DateTimeOffset pickupTime = new DateTimeOffset(new DateTime(2000, 1, 2));
        private DateTimeOffset returnTime = new DateTimeOffset(new DateTime(2000, 1, 3));
        private IAddress fakeAddress = new Mock<IAddress>().Object;
        private Mock<IOwnedBook> fakeOwnedBook = new Mock<IOwnedBook>();

        [Fact]
        public void Loan_Construct_Pass()
        {
            // arrange
            fakeOwnedBook.Setup(b => b.OwnerId).Returns(fakeLender);
            List<IOwnedBook> books = new List<IOwnedBook>() { fakeOwnedBook.Object };

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books);

            // assert
            Assert.NotNull(loan);
        }

        [Fact]
        public void Loan_NotOwnedBookByLender_Fail()
        {
            // arrange
            fakeOwnedBook.Setup(b => b.OwnerId).Returns(fakeBorrower);
            List<IOwnedBook> books = new List<IOwnedBook>() { fakeOwnedBook.Object };

            // act
            ILoan loan() => new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books);

            // assert
            Assert.Throws<ArgumentException>(loan);
        }

        [Fact]
        public void Loan_EmptyBook_Fail()
        {
            // arrange
            List<IOwnedBook> books = new List<IOwnedBook>();

            // act
            ILoan loan() => new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books);

            // assert
            Assert.Throws<ArgumentException>(loan);
        }

        [Fact]
        public void Loan_NullBooks_Fail()
        {
            // arrange

            // act
            ILoan loan() => new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, null);

            // assert
            Assert.Throws<ArgumentNullException>(loan);
        }

        [Fact]
        public void Loan_DefaultLender_Fail()
        {
            // arrange
            fakeOwnedBook.Setup(b => b.OwnerId).Returns(() => default);
            List<IOwnedBook> books = new List<IOwnedBook>() { fakeOwnedBook.Object };

            // act
            ILoan loan() => new Loan(default, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books);

            // assert
            Assert.Throws<ArgumentException>(loan);
        }

        [Fact]
        public void Loan_DefaultBorrower_Fail()
        {
            // arrange
            fakeOwnedBook.Setup(b => b.OwnerId).Returns(fakeBorrower);
            List<IOwnedBook> books = new List<IOwnedBook>() { fakeOwnedBook.Object };

            // act
            ILoan loan() => new Loan(fakeLender, default, message, pickupTime, returnTime, fakeAddress, books);

            // assert
            Assert.Throws<ArgumentException>(loan);
        }

        [Fact]
        public void Loan_ReturnBeforePickup_Fail()
        {
            // arrange
            fakeOwnedBook.Setup(b => b.OwnerId).Returns(fakeBorrower);
            List<IOwnedBook> books = new List<IOwnedBook>() { fakeOwnedBook.Object };

            // act
            ILoan loan() => new Loan(fakeLender, fakeBorrower, message, returnTime, pickupTime, fakeAddress, books);

            // assert
            Assert.Throws<ArgumentException>(loan);
        }

        [Fact]
        public void Loan_NullAddress_Fail()
        {
            // arrange
            fakeOwnedBook.Setup(b => b.OwnerId).Returns(fakeBorrower);
            List<IOwnedBook> books = new List<IOwnedBook>() { fakeOwnedBook.Object };

            // act
            ILoan loan() => new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, null, books);

            // assert
            Assert.Throws<ArgumentNullException>(loan);
        }

        [Fact]
        public void Loan_GetUser()
        {
            // arrange
            fakeOwnedBook.Setup(b => b.OwnerId).Returns(fakeLender);
            List<IOwnedBook> books = new List<IOwnedBook>() { fakeOwnedBook.Object };

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books);

            // assert
            Assert.Equal(fakeLender, loan.Lender);
        }

        [Fact]
        public void Loan_GetBorrower()
        {
            // arrange
            fakeOwnedBook.Setup(b => b.OwnerId).Returns(fakeLender);
            List<IOwnedBook> books = new List<IOwnedBook>() { fakeOwnedBook.Object };

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books);

            // assert
            Assert.Equal(fakeBorrower, loan.Borrower);
        }

        [Fact]
        public void Loan_GetMessage()
        {
            // arrange
            fakeOwnedBook.Setup(b => b.OwnerId).Returns(fakeLender);
            List<IOwnedBook> books = new List<IOwnedBook>() { fakeOwnedBook.Object };

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books);

            // assert
            Assert.Equal(message, loan.Message);
        }

        [Fact]
        public void Loan_GetPickupDate()
        {
            // arrange
            fakeOwnedBook.Setup(b => b.OwnerId).Returns(fakeLender);
            List<IOwnedBook> books = new List<IOwnedBook>() { fakeOwnedBook.Object };

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books);

            // assert
            Assert.Equal(pickupTime, loan.DropoffDate);
        }

        [Fact]
        public void Loan_GetReturnDate()
        {
            // arrange
            fakeOwnedBook.Setup(b => b.OwnerId).Returns(fakeLender);
            List<IOwnedBook> books = new List<IOwnedBook>() { fakeOwnedBook.Object };

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books);

            // assert
            Assert.Equal(returnTime, loan.ReturnDate);
        }

        [Fact]
        public void Loan_GetAddress()
        {
            // arrange
            fakeOwnedBook.Setup(b => b.OwnerId).Returns(fakeLender);
            List<IOwnedBook> books = new List<IOwnedBook>() { fakeOwnedBook.Object };

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books);

            // assert
            Assert.Equal(fakeAddress, loan.ExchangeLocation);
        }

        [Fact]
        public void Loan_GetBooks()
        {
            // arrange
            fakeOwnedBook.Setup(b => b.OwnerId).Returns(fakeLender);
            List<IOwnedBook> books = new List<IOwnedBook>() { fakeOwnedBook.Object };

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books);

            // assert
            Assert.Equal(books, loan.LoanedBooks);
        }
    }
}