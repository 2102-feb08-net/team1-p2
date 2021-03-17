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
            var book = await _context.Books.SingleAsync(b => b.Isbn == ownedBook.Book.Isbn);
            await _context.OwnedBooks.AddAsync(new OwnedBook()
            {
                User = user,
                Book = book,
                Condition = ownedBook.Condition.ToString(),
                AvailabilityStatusId = (int)ownedBook.Availability
            });
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}