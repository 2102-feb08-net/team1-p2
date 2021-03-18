using LooseLeaf.Business.IRepositories;
using LooseLeaf.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LooseLeaf.Business;

namespace LooseLeaf.DataAccess.Repositories
{
    public class OwnedBookRepository : IOwnedBookRepository
    {
        private readonly LooseLeafContext _context;

        public OwnedBookRepository(LooseLeafContext context)
        {
            _context = context;
        }

        public async Task AddOwnedBookAsync(IOwnedBook ownedBook)
        {
            var user = await _context.Users.SingleAsync(u => u.Username == ownedBook.Owner.UserName);
            var book = await _context.Books.SingleOrDefaultAsync(b => b.Isbn == ownedBook.Book.Isbn);
            if (book is null)
            {
                GoogleBooks googleBooks = new GoogleBooks();
                IBook bookObj = await googleBooks.GetBookFromIsbn(ownedBook.Book.Isbn);
                book = await AddBook(bookObj);
            }

            var ownedBookData = new OwnedBook()
            {
                User = user,
                Book = book,
                ConditionId = (int)ownedBook.Condition,
                AvailabilityStatusId = (int)ownedBook.Availability
            };

            await _context.OwnedBooks.AddAsync(ownedBookData);
        }

        public async Task UpdateOwnedBookStatus(int ownedBookId, Availability? availability, PhysicalCondition? condition)
        {
            var ownedBook = await _context.OwnedBooks.SingleAsync(b => b.Id == ownedBookId);
            if (availability.HasValue)
                ownedBook.AvailabilityStatusId = (int)availability;
            if (condition.HasValue)
                ownedBook.ConditionId = (int)condition;
        }

        private async Task<Book> AddBook(IBook book)
        {
            Book newBook = new Book()
            {
                Title = book.Title,
                Author = book.Author,
                Isbn = book.Isbn,
                GenreId = book.GenreId
            };

            await _context.Books.AddAsync(newBook);
            return newBook;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}