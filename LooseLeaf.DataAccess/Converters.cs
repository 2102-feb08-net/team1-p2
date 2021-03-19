using LooseLeaf.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.DataAccess
{
    public static class Converters
    {
        public static IBook ConvertToIBook(this Book book) => new Business.Models.Book(book.Title, book.Author, book.Isbn, book.Genres.Select(g => g.GenreName).ToList());
    }
}