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

        DateTimeOffset PickUpDate { get; }

        DateTimeOffset ReturnDate { get; }

        IAddress ExchangeLocation { get; }

        List<IOwnedBook> LoanedBooks { get; }
    }
}