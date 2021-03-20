using System;
using System.Collections.Generic;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class LoanReview
    {
        public int Id { get; set; }
        public int ReviewerId { get; set; }
        public int LoanId { get; set; }

        public virtual Loan Loan { get; set; }
        public virtual User Reviewer { get; set; }
    }
}
