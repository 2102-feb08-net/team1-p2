using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LooseLeaf.Business.Models;

namespace LooseLeaf.Business.IRepositories
{
    public interface IBookRepository : IRepository
    {
        Task<IEnumerable<IBook>> GetAllBooks(IBookSearchParams searchParams);

        Task<IBook> GetBook(int bookId);
    }
}