using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public class Wishlist : IWishlist
    {
        public List<IBook> Books { get; } = new List<IBook>();
    }
}