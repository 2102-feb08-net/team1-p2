using LooseLeaf.Business.IRepositories;
using LooseLeaf.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LooseLeaf.DataAccess.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly LooseLeafContext _context;

        public WishlistRepository(LooseLeafContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IBook>> GetUserWishlist(int userId)
        {
            var books = await _context.Wishlists
                .Include(w => w.User)
                .Include(w => w.Book)
                .ThenInclude(b => b.Genres)
                .Where(w => w.UserId == userId).Select(w => w.Book).ToListAsync();
            return books.Select(b => b.ConvertToIBook());
        }

        public async Task AddBookToUserWishlist(int userId, int bookId)
        {
            var userData = await _context.Users.Where(u => u.Id == userId).SingleAsync();
            var bookData = await _context.Books.Include(b => b.Wishlists).Where(b => b.Id == bookId).SingleAsync();
            bookData.Wishlists.Add(new Wishlist()
            {
                UserId = userData.Id
            });
        }

        public async Task RemoveBookFromUserWishlist(int userId, int bookId)
        {
            var userData = await _context.Users.Where(u => u.Id == userId).SingleAsync();
            var bookData = await _context.Books.Include(b => b.Wishlists).Where(b => b.Id == bookId).SingleAsync();
            _context.Wishlists.RemoveRange(bookData.Wishlists);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}