using LooseLeaf.Business.IRepositories;
using LooseLeaf.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.DataAccess.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LooseLeafContext _context;

        public LoanRepository(LooseLeafContext context)
        {
            _context = context;
        }

        public async Task AddLoanAsync(ILoan loan)
        {
            var ownedBooks = await _context.OwnedBooks.Where(b => loan.LoanedBookIds.Contains(b.Id)).ToListAsync();

            if (!ownedBooks.TrueForAll(book => book.UserId == loan.Lender))
                throw new ArgumentException("Loan must have the UserId for every OwnedBook match that of the lenderId");

            Loan dataLoan = new Loan()
            {
                LenderId = loan.Lender,
                BorrowerId = loan.Borrower,
                Message = loan.Message,
                DropoffDate = loan.DropoffDate,
                ReturnedDate = loan.ReturnDate,
                LoanStatusId = (int)loan.Status,
                IsPublic = loan.IsPublic
            };

            await _context.Loans.AddAsync(dataLoan);

            List<LoanedBook> loanedBooks = loan.LoanedBookIds.Select(id => new LoanedBook()
            {
                OwnedBookid = id,
                Loan = dataLoan,
            }).ToList();

            await _context.LoanedBooks.AddRangeAsync(loanedBooks);
        }

        public Task GetLoanAsync(ILoanSearchParams searchParams) => throw new NotImplementedException();

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}