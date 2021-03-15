using System;
using System.Collections.Generic;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class LoanStatus
    {
        public LoanStatus()
        {
            Loans = new HashSet<Loan>();
        }

        public int Id { get; set; }
        public string LoanStatus1 { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}
