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
            // Combine to do only one SQL query
            var users = await _context.Users.Where(u => u.Username == loan.Lender.UserName
                                    || u.Username == loan.Borrower.UserName).ToListAsync();

            Loan dataLoan = new Loan()
            {
                LenderId = users.Single(u => u.Username == loan.Lender.UserName).Id,
                BorrowerId = users.Single(u => u.Username == loan.Borrower.UserName).Id,
                Message = loan.Message,
                DropoffDate = loan.DropoffDate,
                ReturnedDate = loan.ReturnDate,
                LoanStatusId = (int)loan.Status,
                IsPublic = loan.IsPublic
            };

            await _context.Loans.AddAsync(dataLoan);

            List<LoanedBook> loanedBooks = loan.LoanedBooks.Select(b => new LoanedBook()
            {
                OwnedBookid = b.Id,
                Loan = dataLoan,
            }).ToList();

            await _context.LoanedBooks.AddRangeAsync(loanedBooks);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}