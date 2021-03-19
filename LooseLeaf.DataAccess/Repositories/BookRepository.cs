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

        public async Task<IEnumerable<IBook>> GetSpecificBooks(IBookSearchParams searchParams) 
        {
            var newParams = searchParams.ToString().ToLower();

            var results = await _context.Books.Where(x => x.Title.ToLower().Contains(newParams) || 
                x.Author.ToLower().Contains(newParams) || x.Genre.GenreName.ToLower().Contains(newParams)).ToListAsync();

            return results.Select(x => ConvertToIBook(x));
        }

        public async Task<IBook> GetBook(int bookId)
        {
            var book = await _context.Books.Where(b => b.Id == bookId).SingleAsync();
            return new Business.Models.Book(book.Id, book.Title, book.Author, book.Isbn, book.GenreId);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task UpdateBook(int bookId) => throw new NotImplementedException();

        private IBook ConvertToIBook(Book book)
        {
            return new Business.Models.Book(book.Title, book.Author, book.Isbn, book.GenreId);
        }
    }
}