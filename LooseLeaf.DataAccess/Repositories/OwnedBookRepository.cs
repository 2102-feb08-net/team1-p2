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

            var ownedBookData = new OwnedBook()
            {
                User = user,
                Book = book,
                ConditionId = (int)ownedBook.Condition,
                AvailabilityStatusId = (int)ownedBook.Availability
            };

            await _context.OwnedBooks.AddAsync(ownedBookData);
        }

        public async Task UpdateOwnedBookStatus(int ownedBookId, Availability availability, PhysicalCondition condition)
        {
            var ownedBook = await _context.OwnedBooks.SingleAsync(b => b.Id == ownedBookId);
            ownedBook.AvailabilityStatusId = (int)availability;
            ownedBook.ConditionId = (int)condition;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}