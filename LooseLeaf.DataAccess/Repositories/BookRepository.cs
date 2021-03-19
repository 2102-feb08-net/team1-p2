using LooseLeaf.Business.IRepositories;
using LooseLeaf.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using LooseLeaf.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LooseLeafContext _context;

        public BookRepository(LooseLeafContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IBook>> GetAllBooks(IBookSearchParams searchParams)
        {
            var newParams = searchParams.ToString().ToLower();

            IQueryable<Book> books = _context.Books.Include(b => b.Genres);

            if (!string.IsNullOrWhiteSpace(searchParams.Author))
                books = books.Where(b => b.Author.ToLower().Contains(searchParams.Author.ToLower()));

            if (!string.IsNullOrWhiteSpace(searchParams.Title))
                books = books.Where(b => b.Title.ToLower().Contains(searchParams.Title.ToLower()));

            if (!string.IsNullOrWhiteSpace(searchParams.Genre))
                books = books.Where(b =>
                    b.Genres.Any(g => g.GenreName.ToLower() == searchParams.Genre.ToLower())
                    );

            var results = await books.ToListAsync();

            return results.Select(x => ConvertToIBook(x));
        }

        public async Task<IBook> GetBook(int bookId)
        {
            var book = await _context.Books.Where(b => b.Id == bookId).SingleAsync();

            var genres = await _context.Genres.Where(g => g.BookId == bookId).Select(g => g.GenreName).ToListAsync();

            return new Business.Models.Book(book.Id, book.Title, book.Author, book.Isbn, genres);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task UpdateBook(int bookId) => throw new NotImplementedException();

        private IBook ConvertToIBook(Book book)
        {
            return new Business.Models.Book(book.Title, book.Author, book.Isbn, book.Genres.Select(g => g.GenreName).ToList());
        }
    }
}