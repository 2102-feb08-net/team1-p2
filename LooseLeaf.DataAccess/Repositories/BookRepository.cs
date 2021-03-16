using LooseLeaf.Business;
using LooseLeaf.Business.Models;
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

        public Task AddBook() => throw new NotImplementedException();

        public Task<IEnumerable<IBook>> GetAllBooks(IBookSearchParams searchParams) => throw new NotImplementedException();

        public Task<IBook> GetBook(int bookId) => throw new NotImplementedException();

        public Task UpdateBook(int bookId) => throw new NotImplementedException();

        public Book Get(int Id) => throw new NotImplementedException();
    }
}