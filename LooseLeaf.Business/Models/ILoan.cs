using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public interface ILoan
    {
        int Lender { get; }
        int Borrower { get; }

        string Message { get; }

        DateTimeOffset DropoffDate { get; }

        DateTimeOffset ReturnDate { get; }

        int ExchangeLocationAddressId { get; }

        LoanStatus Status { get; }

        IReadOnlyCollection<int> LoanedBookIds { get; }
    }
}