using LooseLeaf.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.IRepositories
{
    public interface IWishlistRepository : IRepository
    {
        Task<IEnumerable<IBook>> GetUserWishlist(int userId);

        Task AddBookToUserWishlist(int userId, int bookId);

        Task RemoveBookFromUserWishlist(int userId, int bookId);
    }
}