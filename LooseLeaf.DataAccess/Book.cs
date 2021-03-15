using System;
using System.Collections.Generic;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class Book
    {
        public Book()
        {
            OwnedBooks = new HashSet<OwnedBook>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public int Genreid { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual ICollection<OwnedBook> OwnedBooks { get; set; }
    }
}
