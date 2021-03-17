using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public class Loan : ILoan
    {
        public IUser Lender { get; }

        public IUser Borrower { get; }

        public string Message { get; }

        public DateTimeOffset DropoffDate { get; }

        public DateTimeOffset ReturnDate { get; }

        public IAddress ExchangeLocation { get; }

        public List<IOwnedBook> LoanedBooks { get; }

        public LoanStatus Status { get; }

        public bool IsPublic { get; }

        public Loan(IUser lender, IUser borrower, string message, DateTimeOffset pickUpDate, DateTimeOffset returnDate, IAddress address, List<IOwnedBook> loanedBooks)
        {
            if (lender is null)
                throw new ArgumentNullException(nameof(lender));
            if (borrower is null)
                throw new ArgumentNullException(nameof(borrower));
            if (pickUpDate >= returnDate)
                throw new ArgumentException("The pickup date and time must be before the return date and time.");
            if (address is null)
                throw new ArgumentNullException(nameof(address));
            if (loanedBooks is null)
                throw new ArgumentNullException(nameof(loanedBooks));

            if (loanedBooks.Count == 0)
                throw new ArgumentException("The number of loan books must be at least 1.");

            if (loanedBooks.Find(b => b.Owner != lender) is not null)
                throw new ArgumentException("Every book loaned, must be owned by the lender.");

            Lender = lender;
            Borrower = borrower;
            Message = message;
            DropoffDate = pickUpDate;
            ReturnDate = returnDate;
            ExchangeLocation = address;
            LoanedBooks = loanedBooks;
            Status = LoanStatus.Requested;
            IsPublic = true;
        }
    }
}