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
                AddressId = loan.ExchangeLocationAddressId,
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

        public async Task<IEnumerable<ILoan>> GetLoansAsync(ILoanSearchParams searchParams)
        {
            IQueryable<Loan> loanQuery = _context.Loans.Include(l => l.LoanedBooks).ThenInclude(b => b.OwnedBook).Include(l => l.Address);

            if (searchParams.LenderId.HasValue)
                loanQuery = loanQuery.Where(l => l.LenderId == searchParams.LenderId);
            if (searchParams.BorrowerId.HasValue)
                loanQuery = loanQuery.Where(l => l.BorrowerId == searchParams.BorrowerId);
            if (searchParams.OwnedBookId.HasValue)
                loanQuery = loanQuery.Where(l => l.LoanedBooks.Where(b => b.OwnedBookid == searchParams.OwnedBookId).Any());
            if (searchParams.BookId.HasValue)
                loanQuery = loanQuery.Where(l => l.LoanedBooks.Where(b => b.OwnedBook.BookId == searchParams.BookId).Any());
            if (searchParams.LoanStatus.HasValue)
                loanQuery = loanQuery.Where(l => l.LoanStatusId == (int)searchParams.LoanStatus);

            if (searchParams.Pagination is not null)
                loanQuery = loanQuery.Skip(searchParams.Pagination.PageSize * searchParams.Pagination.PageIndex).Take(searchParams.Pagination.PageIndex);

            var loans = await loanQuery.ToListAsync();

            return loans.Select(l => new Business.Models.Loan(
                l.LenderId,
                l.BorrowerId,
                l.Message,
                l.DropoffDate,
                l.ReturnedDate,
                l.AddressId,
                l.LoanedBooks.Select(b => b.OwnedBookid),
                (Business.Models.LoanStatus)l.LoanStatusId));
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}