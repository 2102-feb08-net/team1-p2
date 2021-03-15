﻿using System;
using System.Collections.Generic;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class OwnedBook
    {
        public OwnedBook()
        {
            LoanedBooks = new HashSet<LoanedBook>();
            Loans = new HashSet<Loan>();
        }

        public int Id { get; set; }
        public int Userid { get; set; }
        public int Bookid { get; set; }
        public string Condition { get; set; }
        public int AvailabilityStatusId { get; set; }

        public virtual AvailabilityStatus AvailabilityStatus { get; set; }
        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<LoanedBook> LoanedBooks { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
