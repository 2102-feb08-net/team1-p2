using System;
using System.Collections.Generic;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class Address
    {
        public Address()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
