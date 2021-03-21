using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LooseLeaf.Business.Models;
using Moq;
using LooseLeaf.DataAccess;
using LooseLeaf.DataAccess.Repositories;

namespace LooseLeaf.Tests.IntegrationTests
{
    public class LoanRespositoryTests
    {
        [Fact]
        public async Task Loans_AddLoan()
        {
            // arrange
            const string LENDER_USERNAME = "lender";
            const string BORROWER_USERNAME = "borrower";

            const int LENDER_ID = 1;
            const int BORROWER_ID = 2;

            const int OWNED_BOOK_ID = 1;

            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                await contextFactory.CreateUser(arrangeContext, LENDER_USERNAME);
                await contextFactory.CreateUser(arrangeContext, BORROWER_USERNAME);
                await contextFactory.CreateBook(arrangeContext, "Book 1", "Author 1");
                await contextFactory.CreateOwnedBook(arrangeContext, 1, 1);
                await arrangeContext.SaveChangesAsync();
            }

            int fakeAddressId = 1;
            var fakeBook = new Mock<IOwnedBook>();
            fakeBook.Setup(b => b.OwnerId).Returns(LENDER_ID);
            fakeBook.Setup(b => b.Id).Returns(1);
            fakeBook.Setup(b => b.Availability).Returns(Availability.Available);

            var ownedBooks = new List<int>()
            {
                OWNED_BOOK_ID
            };

            // act
            using (LooseLeafContext context = contextFactory.CreateContext())
            {
                var loan = new Business.Models.Loan(LENDER_ID, BORROWER_ID, "Hello", new DateTime(2000, 1, 2), new DateTime(2000, 1, 4), fakeAddressId, ownedBooks, Business.Models.LoanStatus.Requested);
                LoanRepository loanRepository = new LoanRepository(context);
                await loanRepository.AddLoanAsync(loan);
                await context.SaveChangesAsync();
            }

            //assert
            using var assertContext = contextFactory.CreateContext();
            Assert.Equal(1, assertContext.Loans.Count());
        }

        [Fact]
        public async Task Loans_GetAllAsync()
        {
            // arrange
            const string LENDER_USERNAME = "lender";
            const string BORROWER_USERNAME = "borrower";
            const string LOAN_MESSAGE = "Hello, I would like to borrow your book.";
            const int LENDER_ID = 1;
            const int BORROWER_ID = 2;

            const int FIRST_OWNED_BOOK_ID = 1;
            const int SECOND_OWNED_BOOK_ID = 2;

            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                await contextFactory.CreateAddress(arrangeContext);
                await contextFactory.CreateUser(arrangeContext, LENDER_USERNAME);
                await contextFactory.CreateUser(arrangeContext, BORROWER_USERNAME);
                await contextFactory.CreateBook(arrangeContext, "Book 1", "Author 1");
                await contextFactory.CreateOwnedBook(arrangeContext, LENDER_ID, FIRST_OWNED_BOOK_ID);
                await contextFactory.CreateBook(arrangeContext, "Book 2", "Author 2");
                await contextFactory.CreateOwnedBook(arrangeContext, LENDER_ID, SECOND_OWNED_BOOK_ID);

                var loan = new DataAccess.Loan()
                {
                    LenderId = LENDER_ID,
                    BorrowerId = BORROWER_ID,
                    Message = LOAN_MESSAGE,
                    LoanStatusId = (int)Business.Models.LoanStatus.Approved,
                    AddressId = 1,
                    DropoffDate = new DateTime(2000, 10, 1),
                    ReturnedDate = new DateTime(2000, 10, 17),
                };

                var loanedBooks = new List<DataAccess.LoanedBook>() {
                    new LoanedBook() { Loan = loan, OwnedBookid = FIRST_OWNED_BOOK_ID },
                    new LoanedBook() { Loan = loan, OwnedBookid = SECOND_OWNED_BOOK_ID }
                };

                await arrangeContext.AddAsync(loan);
                await arrangeContext.AddRangeAsync(loanedBooks);
                await arrangeContext.SaveChangesAsync();
            }

            // act
            IEnumerable<ILoanResult> loans;
            using (LooseLeafContext actContext = contextFactory.CreateContext())
            {
                LoanRepository loanRepository = new LoanRepository(actContext);
                ILoanSearchParams searchParams = new LoanSearchParams();
                loans = await loanRepository.GetLoansAsync(searchParams);
            }

            // assert
            var firstLoan = loans.First();
            Assert.Single(loans);
            Assert.Equal(LENDER_ID, firstLoan.Lender.Id);
            Assert.Equal(BORROWER_ID, firstLoan.Borrower.Id);
            Assert.Equal(LOAN_MESSAGE, firstLoan.Message);
            Assert.Equal(FIRST_OWNED_BOOK_ID, firstLoan.LoanedBooks.First().Id);
            Assert.Equal(SECOND_OWNED_BOOK_ID, firstLoan.LoanedBooks.Last().Id);
        }

        // get loan by id

        // submit a loan request

        // update the satus of a loan

        // get loans by userId
    }
}