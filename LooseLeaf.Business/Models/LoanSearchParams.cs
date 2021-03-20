using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public class LoanSearchParams : ILoanSearchParams
    {
        public LoanStatus? LoanStatus { get; set; }
        public int? LenderId { get; set; }
        public int? BorrowerId { get; set; }
        public int? OwnedBookId { get; set; }
        public int? BookId { get; set; }
        public IPagination Pagination { get; set; }
    }
}