using System;
using System.Collections.Generic;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class Book
    {
        public Book()
        {
            Genres = new HashSet<Genre>();
            OwnedBooks = new HashSet<OwnedBook>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public long Isbn { get; set; }
        public string Thumbnail { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<OwnedBook> OwnedBooks { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
