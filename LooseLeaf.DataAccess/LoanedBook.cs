using System;
using System.Collections.Generic;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class LoanedBook
    {
        public int Id { get; set; }
        public int OwnedBookid { get; set; }
        public int Loanid { get; set; }

        public virtual Loan Loan { get; set; }
        public virtual OwnedBook OwnedBook { get; set; }
    }
}
