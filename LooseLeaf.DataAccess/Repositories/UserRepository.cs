using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LooseLeaf.Business.IRepositories;
using LooseLeaf.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace LooseLeaf.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LooseLeafContext _context;

        public UserRepository(LooseLeafContext context)
        {
            _context = context;
        }

        public async Task<int> AddUserAsync(INewUser user)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.AuthId == user.AuthId);

            if (existingUser is not null)
                return existingUser.Id;

            User newUser = new User()
            {
                Username = user.Username,
                AuthId = user.AuthId,
                Email = user.Email,
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser.Id;
        }

        public async Task<IEnumerable<IUser>> GetAllUsersAsync()
        {
            var userList = await _context.Users.Select(u =>
            new Business.Models.User(
                    u.Id,
                    u.Username,
                    u.Email
                )).ToListAsync();
            return userList;
        }

        public async Task<IEnumerable<IBook>> GetRecommendedBooksAsync(int userid)
        {
            var loans = _context.Loans.Where(b => b.BorrowerId == userid)
            .Include(b => b.LoanedBooks).ThenInclude(b => b.OwnedBook)
            .ThenInclude(b => b.Book).ThenInclude(b => b.Genres);
            var loanbooks = loans.Select(b => b.LoanedBooks).ToList().SelectMany(g => g);
            var ownedbooks = loanbooks.Select(b => b.OwnedBook).ToList();
            var booklist = ownedbooks.Select(b => b.Book).ToList();
            var _genre = booklist.Select(b => b.Genres).AsEnumerable().SelectMany(b => b);

            if (loans.Any().Equals(0))
            {
                return _context.Books.Include(b => b.Genres).Take(5).Select(b => b.ConvertToIBook()).ToList();
            }
            else
            {
                var grouped = _genre.GroupBy(item => item);
                var items = grouped.SelectMany(g => g);
                string name = items.First().GenreName;
                var genre = _context.Genres.Where(g => g.GenreName.Equals(name)).FirstOrDefault();
                return _context.Books.Include(b => b.Genres).Where(g => g.Genres.Contains(genre)).Take(5).Select(b => b.ConvertToIBook()).ToList();
            }

            //checks to see if the list is empty. if it is empty, grab the first five books in the database and suggest them.
        }

        public async Task<IUser> GetUserAsync(int userid)
        {
            var user = await _context.Users.Where(b => b.Id == userId).SingleAsync();
            return new Business.Models.User(userId, user.Username, user.Email);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}