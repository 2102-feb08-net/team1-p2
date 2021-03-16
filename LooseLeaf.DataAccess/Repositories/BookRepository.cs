using LooseLeaf.Business;
using LooseLeaf.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task AddBook() => throw new NotImplementedException();

        public async Task<IEnumerable<IBook>> GetAllBooks(IBookSearchParams searchParams) => throw new NotImplementedException();

        public async Task<IBook> GetBook(int bookId)
        {
            var book = await _context.Books.Where(b => b.Id == bookId).SingleAsync();
            return new Business.Models.Book(book.Id, book.Title, book.Author, book.Isbn, book.GenreId);
        }

        public async Task UpdateBook(int bookId) => throw new NotImplementedException();
    }
}