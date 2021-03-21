using System;
using System.Collections.Generic;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class User
    {
        public User()
        {
            LoanBorrowers = new HashSet<Loan>();
            LoanLenders = new HashSet<Loan>();
            LoanReviews = new HashSet<LoanReview>();
            OwnedBooks = new HashSet<OwnedBook>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int Id { get; set; }
        public string AuthId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Loan> LoanBorrowers { get; set; }
        public virtual ICollection<Loan> LoanLenders { get; set; }
        public virtual ICollection<LoanReview> LoanReviews { get; set; }
        public virtual ICollection<OwnedBook> OwnedBooks { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
