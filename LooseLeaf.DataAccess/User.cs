using System;
using System.Collections.Generic;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class User
    {
        public User()
        {
            Loans = new HashSet<Loan>();
            OwnedBooks = new HashSet<OwnedBook>();
        }

        public int Id { get; set; }
        public int Addressid { get; set; }
        public string Username { get; set; }
        public string Userpassword { get; set; }
        public string Email { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<OwnedBook> OwnedBooks { get; set; }
    }
}
