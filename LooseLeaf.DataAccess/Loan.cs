using System;
using System.Collections.Generic;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class Loan
    {
        public Loan()
        {
            LoanedBooks = new HashSet<LoanedBook>();
        }

        public int Id { get; set; }
        public int LenderId { get; set; }
        public int BorrowerId { get; set; }
        public int OwnedBookid { get; set; }
        public string Message { get; set; }
        public int LoanStatusId { get; set; }
        public bool IsPublic { get; set; }
        public DateTime DropoffDate { get; set; }
        public DateTime ReturnedDate { get; set; }
        public bool IsRecommended { get; set; }

        public virtual User Borrower { get; set; }
        public virtual User Lender { get; set; }
        public virtual LoanStatus LoanStatus { get; set; }
        public virtual OwnedBook OwnedBook { get; set; }
        public virtual ICollection<LoanedBook> LoanedBooks { get; set; }
    }
}
