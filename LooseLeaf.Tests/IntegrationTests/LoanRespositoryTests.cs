using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LooseLeaf.Business.Models;
using Moq;
using LooseLeaf.DataAccess;

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
            using var contextFactory = new TestLooseLeafContextFactory();
            using LooseLeafContext context = contextFactory.CreateContext();

            IUser lender = new Mock<IUser>().Object;
            IUser borrower = new Mock<IUser>().Object;
            IAddress fakeAddress = new Mock<IAddress>().Object;
            var fakeBook = new Mock<IOwnedBook>();
            fakeBook.Setup(b => b.Owner).Returns(lender);

            var ownedBooks = new List<IOwnedBook>()
            {
                fakeBook.Object
            };

            var loans = new List<ILoan>()
            {
                new Business.Models.Loan(lender,borrower, "Hello", new DateTime(2000,1,2), new DateTime(2000,1,4), fakeAddress, ownedBooks)
            };

            // act
            // TODO: Add Loan Repository call

            await context.SaveChangesAsync();

            //assert
            Assert.Equal(loans.Count, context.Loans.Count());
        }

        // get loan by id

        // submit a loan request

        // update the satus of a loan

        // get loans by userId
    }
}