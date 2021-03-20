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
        private int fakeAddress = 4;
        private List<int> books = new List<int>() { 1, 2, 10 };
        private const LoanStatus status = LoanStatus.Requested;

        [Fact]
        public void Loan_Construct_Pass()
        {
            // arrange

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.NotNull(loan);
        }

        [Fact]
        public void Loan_EmptyBook_Fail()
        {
            // arrange
            List<int> books = new List<int>();

            // act
            ILoan loan() => new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Throws<ArgumentException>(loan);
        }

        [Fact]
        public void Loan_NullBooks_Fail()
        {
            // arrange

            // act
            ILoan loan() => new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, null, status);

            // assert
            Assert.Throws<ArgumentNullException>(loan);
        }

        [Fact]
        public void Loan_DefaultLender_Fail()
        {
            // arrange

            // act
            ILoan loan() => new Loan(default, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Throws<ArgumentException>(loan);
        }

        [Fact]
        public void Loan_DefaultBorrower_Fail()
        {
            // arrange

            // act
            ILoan loan() => new Loan(fakeLender, default, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Throws<ArgumentException>(loan);
        }

        [Fact]
        public void Loan_ReturnBeforePickup_Fail()
        {
            // arrange

            // act
            ILoan loan() => new Loan(fakeLender, fakeBorrower, message, returnTime, pickupTime, fakeAddress, books, status);

            // assert
            Assert.Throws<ArgumentException>(loan);
        }

        [Fact]
        public void Loan_DefaultAddress_Fail()
        {
            // arrange

            // act
            ILoan loan() => new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, default, books, status);

            // assert
            Assert.Throws<ArgumentException>(loan);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1000)]
        public void Loan_InvalidStatus_Fail(int invalidStatus)
        {
            // arrange

            // act
            ILoan loan() => new Loan(fakeLender, fakeBorrower, message, returnTime, pickupTime, fakeAddress, books, (LoanStatus)invalidStatus);

            // assert
            Assert.Throws<ArgumentException>(loan);
        }

        [Fact]
        public void Loan_GetUser()
        {
            // arrange

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(fakeLender, loan.Lender);
        }

        [Fact]
        public void Loan_GetBorrower()
        {
            // arrange

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(fakeBorrower, loan.Borrower);
        }

        [Fact]
        public void Loan_GetMessage()
        {
            // arrange

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(message, loan.Message);
        }

        [Fact]
        public void Loan_GetPickupDate()
        {
            // arrange

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(pickupTime, loan.DropoffDate);
        }

        [Fact]
        public void Loan_GetReturnDate()
        {
            // arrange

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(returnTime, loan.ReturnDate);
        }

        [Fact]
        public void Loan_GetAddress()
        {
            // arrange

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(fakeAddress, loan.ExchangeLocationAddressId);
        }

        [Fact]
        public void Loan_GetBooks()
        {
            // arrange

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(books, loan.LoanedBookIds);
        }

        [Fact]
        public void Loan_GetStatus()
        {
            // arrange

            // act
            ILoan loan = new Loan(fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(status, loan.Status);
        }
    }
}