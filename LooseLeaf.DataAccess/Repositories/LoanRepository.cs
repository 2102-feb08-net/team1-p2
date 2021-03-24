using LooseLeaf.Business.IRepositories;
using LooseLeaf.Business.Models;
using LooseLeaf.Business.Models.Results;
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
            };

            await _context.Loans.AddAsync(dataLoan);

            List<LoanedBook> loanedBooks = loan.LoanedBookIds.Select(id => new LoanedBook()
            {
                OwnedBookid = id,
                Loan = dataLoan,
            }).ToList();

            await _context.LoanedBooks.AddRangeAsync(loanedBooks);
        }

        public async Task<ILoanResult> GetLoanById(int loanId)
        {
            var loan = await _context.Loans
                .Include(l => l.Lender)
                .Include(l => l.Borrower)
                .Include(l => l.LoanedBooks)
                    .ThenInclude(b => b.OwnedBook)
                        .ThenInclude(b => b.AvailabilityStatus)
                .Include(l => l.LoanedBooks)
                    .ThenInclude(b => b.OwnedBook)
                        .ThenInclude(b => b.Condition)
                .Include(l => l.LoanedBooks)
                    .ThenInclude(b => b.OwnedBook)
                        .ThenInclude(b => b.Book)
                .Include(l => l.Address)
                .Include(l => l.LoanStatus)
                .SingleAsync(loan => loan.Id == loanId);

            return new Business.Models.LoanResult(
                loan.Id,
                new UserResult(loan.Lender.Id, loan.Lender.Username),
                new UserResult(loan.Borrower.Id, loan.Borrower.Username),
                loan.Message,
                loan.DropoffDate,
                loan.ReturnedDate,
                loan.Address.ConvertToIAddress(),
                loan.LoanedBooks.Select(b => new OwnedBookResult(b.OwnedBook.Id, b.OwnedBook.Book.ConvertToIBook(), b.OwnedBook.UserId, b.OwnedBook.Condition.StatusName, b.OwnedBook.AvailabilityStatus.StatusName)),
                loan.LoanStatus.StatusName);
        }

        public async Task<IEnumerable<ILoanResult>> GetLoansAsync(ILoanSearchParams searchParams)
        {
            IQueryable<Loan> loanQuery = _context.Loans
                .Include(l => l.Lender)
                .Include(l => l.Borrower)
                .Include(l => l.LoanedBooks)
                    .ThenInclude(b => b.OwnedBook)
                        .ThenInclude(b => b.AvailabilityStatus)
                .Include(l => l.LoanedBooks)
                    .ThenInclude(b => b.OwnedBook)
                        .ThenInclude(b => b.Condition)
                .Include(l => l.LoanedBooks)
                    .ThenInclude(b => b.OwnedBook)
                        .ThenInclude(b => b.Book)
                .Include(l => l.Address)
                .Include(l => l.LoanStatus);

            if (searchParams.LenderId.HasValue)
                loanQuery = loanQuery.Where(l => l.LenderId == searchParams.LenderId);
            if (searchParams.BorrowerId.HasValue)
                loanQuery = loanQuery.Where(l => l.BorrowerId == searchParams.BorrowerId);
            if (searchParams.AnyUserId.HasValue)
                loanQuery = loanQuery.Where(l => searchParams.AnyUserId == l.Borrower.Id || searchParams.AnyUserId == l.Lender.Id);

            if (searchParams.OwnedBookId.HasValue)
                loanQuery = loanQuery.Where(l => l.LoanedBooks.Any(b => b.OwnedBookid == searchParams.OwnedBookId));
            if (searchParams.BookId.HasValue)
                loanQuery = loanQuery.Where(l => l.LoanedBooks.Any(b => b.OwnedBook.BookId == searchParams.BookId));
            if (searchParams.LoanStatus.HasValue)
                loanQuery = loanQuery.Where(l => l.LoanStatusId == (int)searchParams.LoanStatus);

            if (searchParams.Pagination is not null)
                loanQuery = loanQuery.Skip(searchParams.Pagination.PageSize * searchParams.Pagination.PageIndex).Take(searchParams.Pagination.PageIndex);

            var loans = await loanQuery.ToListAsync();

            return loans.Select(l => new Business.Models.LoanResult(
                l.Id,
                new UserResult(l.Lender.Id, l.Lender.Username),
                new UserResult(l.Borrower.Id, l.Borrower.Username),
                l.Message,
                l.DropoffDate,
                l.ReturnedDate,
                l.Address.ConvertToIAddress(),
                l.LoanedBooks.Select(b => new OwnedBookResult(b.OwnedBook.Id, b.OwnedBook.Book.ConvertToIBook(), b.OwnedBook.UserId, b.OwnedBook.Condition.StatusName, b.OwnedBook.AvailabilityStatus.StatusName)),
                l.LoanStatus.StatusName));
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task UpdateLoanStatusAsync(int loanId, Business.Models.LoanStatus newStatus)
        {
            var loan = await _context.Loans.SingleAsync(loan => loan.Id == loanId);
            loan.LoanStatusId = (int)newStatus;
        }
    }
}