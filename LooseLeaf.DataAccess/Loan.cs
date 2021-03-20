using System;
using System.Collections.Generic;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class Loan
    {
        public Loan()
        {
            LoanReviews = new HashSet<LoanReview>();
            LoanedBooks = new HashSet<LoanedBook>();
        }

        public int Id { get; set; }
        public int LenderId { get; set; }
        public int BorrowerId { get; set; }
        public string Message { get; set; }
        public int LoanStatusId { get; set; }
        public bool IsPublic { get; set; }
        public DateTimeOffset DropoffDate { get; set; }
        public DateTimeOffset ReturnedDate { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual User Borrower { get; set; }
        public virtual User Lender { get; set; }
        public virtual LoanStatus LoanStatus { get; set; }
        public virtual ICollection<LoanReview> LoanReviews { get; set; }
        public virtual ICollection<LoanedBook> LoanedBooks { get; set; }
    }
}
