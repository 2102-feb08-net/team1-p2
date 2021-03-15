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
        public int Userid { get; set; }
        public int OwnedBookid { get; set; }
        public string Message { get; set; }
        public int LoanStatusId { get; set; }
        public bool Ispublic { get; set; }
        public DateTime Dropoffdate { get; set; }
        public DateTime Returneddate { get; set; }
        public bool IsRecommended { get; set; }

        public virtual LoanStatus LoanStatus { get; set; }
        public virtual OwnedBook OwnedBook { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<LoanedBook> LoanedBooks { get; set; }
    }
}
