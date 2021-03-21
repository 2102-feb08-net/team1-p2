using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LooseLeaf.Business.Models;
using Moq;
using LooseLeaf.Business.Models.Results;

namespace LooseLeaf.Tests
{
    public class LoanResultTests
    {
        private const int id = 1;
        private IUserResult fakeLender = new Mock<IUserResult>().Object;
        private IUserResult fakeBorrower = new Mock<IUserResult>().Object;
        private const string message = "I want to borrow these books.";
        private readonly DateTimeOffset pickupTime = new DateTimeOffset(new DateTime(2000, 1, 2));
        private readonly DateTimeOffset returnTime = new DateTimeOffset(new DateTime(2000, 1, 3));
        private readonly IAddress fakeAddress = new Mock<IAddress>().Object;
        private readonly List<IOwnedBookResult> books = new List<IOwnedBookResult>() { new Mock<IOwnedBookResult>().Object, new Mock<IOwnedBookResult>().Object };
        private const string status = "Available";

        [Fact]
        public void Loan_Construct_Pass()
        {
            // arrange

            // act
            ILoanResult loan = new LoanResult(id, fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.NotNull(loan);
        }

        [Fact]
        public void Loan_EmptyBook_Fail()
        {
            // arrange
            List<IOwnedBookResult> books = new List<IOwnedBookResult>();

            // act
            ILoanResult loan() => new LoanResult(id, fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Throws<ArgumentException>(loan);
        }

        [Fact]
        public void Loan_NullBooks_Fail()
        {
            // arrange

            // act
            ILoanResult loan() => new LoanResult(id, fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, null, status);

            // assert
            Assert.Throws<ArgumentNullException>(loan);
        }

        [Fact]
        public void Loan_DefaultLender_Fail()
        {
            // arrange

            // act
            ILoanResult loan() => new LoanResult(id, default, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Throws<ArgumentNullException>(loan);
        }

        [Fact]
        public void Loan_DefaultBorrower_Fail()
        {
            // arrange

            // act
            ILoanResult loan() => new LoanResult(id, fakeLender, default, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Throws<ArgumentNullException>(loan);
        }

        [Fact]
        public void Loan_ReturnBeforePickup_Fail()
        {
            // arrange

            // act
            ILoanResult loan() => new LoanResult(id, fakeLender, fakeBorrower, message, returnTime, pickupTime, fakeAddress, books, status);

            // assert
            Assert.Throws<ArgumentException>(loan);
        }

        [Fact]
        public void Loan_DefaultAddress_Fail()
        {
            // arrange

            // act
            ILoanResult loan() => new LoanResult(id, fakeLender, fakeBorrower, message, pickupTime, returnTime, default, books, status);

            // assert
            Assert.Throws<ArgumentNullException>(loan);
        }

        [Fact]
        public void Loan_GetUser()
        {
            // arrange

            // act
            ILoanResult loan = new LoanResult(id, fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(fakeLender, loan.Lender);
        }

        [Fact]
        public void Loan_GetBorrower()
        {
            // arrange

            // act
            ILoanResult loan = new LoanResult(id, fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(fakeBorrower, loan.Borrower);
        }

        [Fact]
        public void Loan_GetMessage()
        {
            // arrange

            // act
            ILoanResult loan = new LoanResult(id, fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(message, loan.Message);
        }

        [Fact]
        public void Loan_GetPickupDate()
        {
            // arrange

            // act
            ILoanResult loan = new LoanResult(id, fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(pickupTime, loan.StartDate);
        }

        [Fact]
        public void Loan_GetReturnDate()
        {
            // arrange

            // act
            ILoanResult loan = new LoanResult(id, fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(returnTime, loan.EndDate);
        }

        [Fact]
        public void Loan_GetAddress()
        {
            // arrange

            // act
            ILoanResult loan = new LoanResult(id, fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(fakeAddress, loan.ExchangeLocationAddress);
        }

        [Fact]
        public void Loan_GetBooks()
        {
            // arrange

            // act
            ILoanResult loan = new LoanResult(id, fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(books, loan.LoanedBooks);
        }

        [Fact]
        public void Loan_GetStatus()
        {
            // arrange

            // act
            ILoanResult loan = new LoanResult(id, fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(status, loan.Status);
        }

        [Fact]
        public void Loan_GetId()
        {
            // arrange

            // act
            ILoanResult loan = new LoanResult(id, fakeLender, fakeBorrower, message, pickupTime, returnTime, fakeAddress, books, status);

            // assert
            Assert.Equal(id, loan.Id);
        }
    }
}