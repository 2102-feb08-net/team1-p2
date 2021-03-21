using LooseLeaf.Business.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public interface ILoanResult
    {
        int Id { get; }
        IUserResult Lender { get; }
        IUserResult Borrower { get; }

        string Message { get; }

        DateTimeOffset StartDate { get; }

        DateTimeOffset EndDate { get; }

        IAddress ExchangeLocationAddress { get; }

        string Status { get; }

        IReadOnlyCollection<IOwnedBookResult> LoanedBooks { get; }
    }
}