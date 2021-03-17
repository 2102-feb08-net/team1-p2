using System;
using System.Collections.Generic;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class ConditionStatus
    {
        public ConditionStatus()
        {
            OwnedBooks = new HashSet<OwnedBook>();
        }

        public int Id { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<OwnedBook> OwnedBooks { get; set; }
    }
}
