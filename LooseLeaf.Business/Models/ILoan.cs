using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public interface ILoan
    {
        IUser Lender { get; }
        IUser Borrower { get; }

        string Message { get; }

        DateTimeOffset DropoffDate { get; }

        DateTimeOffset ReturnDate { get; }

        IAddress ExchangeLocation { get; }

        LoanStatus Status { get; }

        bool IsPublic { get; }

        List<IOwnedBook> LoanedBooks { get; }
    }
}