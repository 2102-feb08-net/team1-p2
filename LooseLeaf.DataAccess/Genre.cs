using System;
using System.Collections.Generic;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class Genre
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string GenreName { get; set; }

        public virtual Book Book { get; set; }
    }
}
