using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public class Loan : ILoan
    {
        public int Lender { get; }

        public int Borrower { get; }

        public string Message { get; }

        public DateTimeOffset DropoffDate { get; }

        public DateTimeOffset ReturnDate { get; }

        public int ExchangeLocationAddressId { get; }

        public IReadOnlyCollection<int> LoanedBookIds { get; }

        public LoanStatus Status { get; }

        public Loan(int lenderId, int borrowerId, string message, DateTimeOffset pickUpDate, DateTimeOffset returnDate, int addressId, IEnumerable<int> loanedBooks, LoanStatus status)
        {
            if (lenderId <= 0)
                throw new ArgumentException("The lenderId must be greater than 0.", nameof(lenderId));
            if (borrowerId <= 0)
                throw new ArgumentException("The borrowId must be greater than 0.", nameof(borrowerId));
            if (pickUpDate >= returnDate)
                throw new ArgumentException("The pickup date and time must be before the return date and time.");
            if (addressId <= 0)
                throw new ArgumentException("The addressId must be greater than 0.", nameof(addressId));
            if (loanedBooks is null)
                throw new ArgumentNullException(nameof(loanedBooks));

            if (!loanedBooks.Any())
                throw new ArgumentException("The number of loan books must be at least 1.");

            if (loanedBooks.Distinct().Count() != loanedBooks.Count())
                throw new ArgumentException("A loan cannot contain multiple of the same owned book.");

            if (!Enum.IsDefined(status))
                throw new ArgumentException("The loan status must be a valid value.");

            Lender = lenderId;
            Borrower = borrowerId;
            Message = message;
            DropoffDate = pickUpDate;
            ReturnDate = returnDate;
            ExchangeLocationAddressId = addressId;
            LoanedBookIds = new ReadOnlyCollection<int>(loanedBooks.ToList());
            Status = status;
        }
    }
}