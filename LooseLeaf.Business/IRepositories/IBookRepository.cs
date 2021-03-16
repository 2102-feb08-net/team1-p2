using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LooseLeaf.Business.Models;

namespace LooseLeaf.Business
{
    public interface IBookRepository
    {
        Task<IEnumerable<IBook>> GetAllBooks(IBookSearchParams searchParams);

        Task AddBook();

        Task<IBook> GetBook(int bookId);

        Task UpdateBook(int bookId);
    }
}