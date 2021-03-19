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
        // get all loans
        [Fact]
        public void Loans_GetAll()
        {
            // arrange

            // act

            // assert
        }

        [Fact]
        public async Task Loans_AddLoan()
        {
            // arrange
            const string LENDER_USERNAME = "lender";
            const string BORROWER_USERNAME = "borrower";

            const int LENDER_ID = 1;
            const int BORROWER_ID = 2;

            const int OWNED_BOOK_ID = 1;

            ILoan loan;
            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                await contextFactory.CreateUser(arrangeContext, LENDER_USERNAME);
                await contextFactory.CreateUser(arrangeContext, BORROWER_USERNAME);
                await contextFactory.CreateBook(arrangeContext, "Book 1", "Author 1");
                await contextFactory.CreateOwnedBook(arrangeContext, 1, 1);
                await arrangeContext.SaveChangesAsync();
            }

            IAddress fakeAddress = new Mock<IAddress>().Object;
            var fakeBook = new Mock<IOwnedBook>();
            fakeBook.Setup(b => b.OwnerId).Returns(LENDER_ID);
            fakeBook.Setup(b => b.Id).Returns(1);
            fakeBook.Setup(b => b.Availability).Returns(Availability.Available);

            var ownedBooks = new List<int>()
            {
                OWNED_BOOK_ID
            };

            loan = new Business.Models.Loan(LENDER_ID, BORROWER_ID, "Hello", new DateTime(2000, 1, 2), new DateTime(2000, 1, 4), fakeAddress, ownedBooks, Business.Models.LoanStatus.Requested);

            // act
            using (LooseLeafContext context = contextFactory.CreateContext())
            {
                LoanRepository loanRepository = new LoanRepository(context);
                await loanRepository.AddLoanAsync(loan);
                await context.SaveChangesAsync();
            }

            //assert
            using var assertContext = contextFactory.CreateContext();
            Assert.Equal(1, assertContext.Loans.Count());
        }

        // get loan by id

        // submit a loan request

        // update the satus of a loan

        // get loans by userId
    }
}