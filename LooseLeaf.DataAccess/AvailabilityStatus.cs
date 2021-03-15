using System;
using System.Collections.Generic;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class AvailabilityStatus
    {
        public AvailabilityStatus()
        {
            OwnedBooks = new HashSet<OwnedBook>();
        }

        public int Id { get; set; }
        public string AvailabilityStatus1 { get; set; }

        public virtual ICollection<OwnedBook> OwnedBooks { get; set; }
    }
}
