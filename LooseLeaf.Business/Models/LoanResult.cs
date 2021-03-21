using LooseLeaf.Business.Models.Results;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public class LoanResult : ILoanResult
    {
        public int Id { get; }

        public IUserResult Lender { get; }

        public IUserResult Borrower { get; }

        public string Message { get; }

        public DateTimeOffset StartDate { get; }

        public DateTimeOffset EndDate { get; }

        public IAddress ExchangeLocationAddress { get; }

        public IReadOnlyCollection<IOwnedBookResult> LoanedBooks { get; }
        public string Status { get; }

        public LoanResult(int id, IUserResult lender, IUserResult borrower, string message, DateTimeOffset startDate, DateTimeOffset endDate, IAddress address, IEnumerable<IOwnedBookResult> loanedBooks, string loanStatus)
        {
            if (id <= 0)
                throw new ArgumentException("The id must be greater than 0.", nameof(id));
            if (lender is null)
                throw new ArgumentNullException(nameof(lender), "The lenderId must not be null.");
            if (borrower is null)
                throw new ArgumentNullException(nameof(borrower), "The borrowId must be not be null.");
            if (startDate >= endDate)
                throw new ArgumentException("The pickup date and time must be before the return date and time.");
            if (address is null)
                throw new ArgumentNullException(nameof(address), "The address must not be null.");
            if (loanedBooks is null)
                throw new ArgumentNullException(nameof(loanedBooks));

            if (!loanedBooks.Any())
                throw new ArgumentException("The number of loan books must be at least 1.");

            if (loanedBooks.Distinct().Count() != loanedBooks.Count())
                throw new ArgumentException("A loan cannot contain multiple of the same owned book.");

            if (string.IsNullOrWhiteSpace(loanStatus))
                throw new ArgumentException("The loan status must not be empty.");

            Id = id;
            Lender = lender;
            Borrower = borrower;
            Message = message;
            StartDate = startDate;
            EndDate = endDate;
            ExchangeLocationAddress = address;
            LoanedBooks = new ReadOnlyCollection<IOwnedBookResult>(loanedBooks.ToList());
            Status = loanStatus;
        }
    }
}