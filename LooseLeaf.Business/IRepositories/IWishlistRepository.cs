using LooseLeaf.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.IRepositories
{
    public interface IWishlistRepository
    {
        Task<IEnumerable<IBook>> GetUserWishlist(IUser user);

        Task AddBookToUserWishlist(IUser user, IBook book);

        Task RemoveBookFromUserWishlist(IUser user, IBook book);
    }
}