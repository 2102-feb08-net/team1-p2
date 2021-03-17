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

        public async Task<IEnumerable<IBook>> GetUserWishlist(IUser user)
        {
            var books = await _context.Wishlists
                .Include(w => w.User)
                .Include(w => w.Book)
                .Where(w => w.User.Username == user.UserName).Select(w => w.Book).ToListAsync();
            return books.Select(b => ConvertToIBook(b));
        }

        public async Task AddBookToUserWishlist(IUser user, IBook book)
        {
            var userData = await _context.Users.Where(u => u.Username == user.UserName).SingleAsync();
            var bookData = await _context.Books.Include(b => b.Wishlists).Where(b => b.Isbn == book.Isbn).SingleAsync();
            bookData.Wishlists.Add(new Wishlist()
            {
                UserId = userData.Id
            });
        }

        public async Task RemoveBookFromUserWishlist(IUser user, IBook book)
        {
            var userData = await _context.Users.Where(u => u.Username == user.UserName).SingleAsync();
            var bookData = await _context.Books.Include(b => b.Wishlists).Where(b => b.Isbn == book.Isbn).SingleAsync();
            _context.Wishlists.RemoveRange(bookData.Wishlists);
        }

        private IBook ConvertToIBook(Book book)
        {
            return new Business.Models.Book(book.Title, book.Author, book.Isbn, book.GenreId);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}