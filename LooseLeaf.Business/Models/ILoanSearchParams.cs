using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public interface ILoanSearchParams : ISearchParams
    {
        int? LenderId { get; set; }
        int? BorrowerId { get; set; }
        int? OwnedBookId { get; set; }

        int? AnyUserId { get; set; }

        int? BookId { get; set; }
        LoanStatus? LoanStatus { get; set; }
    }
}