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

        public async Task AddUserAsync(IUser user)
        {
            var userAddress = user.Address;

            Address newUserAddress = await _context.Addresses.Where(a =>
                a.Address1 == userAddress.Address1 &&
                a.Address2 == userAddress.Address2 &&
                a.City == userAddress.City &&
                a.State == userAddress.State &&
                a.Zipcode == userAddress.ZipCode
            ).FirstOrDefaultAsync()
            ?? new Address()
            {
                Address1 = userAddress.Address1,
                Address2 = userAddress.Address2,
                City = userAddress.City,
                State = userAddress.State,
                Zipcode = userAddress.ZipCode
            };

            User newUser = new User()
            {
                Username = user.UserName,
                Email = user.Email,
                Address = newUserAddress
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IUser>> GetAllUsersAsync()
        {
            var userList = await _context.Users.Include(u => u.Address).Select(u =>
            new Business.Models.User(
                    u.Id,
                    u.Username,
                    u.Email,
                    new Business.Models.Address(u.Address.Address1, u.Address.Address2, u.Address.City, u.Address.State, u.Address.Country, u.Address.Zipcode)
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
            var _genre = booklist.Select(b => b.Genres).ToList().SelectMany(b => b);
            
            
            
            if(loans.Count().Equals(0)){
                return _context.Books.Include(b => b.Genres).Take(5).Select(b => b.ConvertToIBook()).ToList();
            }
            else{      
        
                var grouped = _genre.GroupBy(item => item);
                var sorted = grouped.OrderByDescending(group => group.Count()).First();
                var items = grouped.SelectMany(g => g);
                string name = items.First().GenreName;
                var genre = _context.Genres.Where(g => g.GenreName.Equals(name)).FirstOrDefault();
                return _context.Books.Include(b => b.Genres).Where(g => g.Genres.Contains(genre)).Take(5).Select(b => b.ConvertToIBook()).ToList();
            }



             //checks to see if the list is empty. if it is empty, grab the first five books in the database and suggest them.
            
            
            
        
           
           
           
        }

        public async Task<IUser> GetUserAsync(int userId)
        {
            var user = await _context.Users.Where(b => b.Id == userId).SingleAsync();
            var useraddress = await _context.Addresses.Where(b => b.Id == user.AddressId).SingleAsync();
            Business.Models.Address address = new Business.Models.Address(useraddress.Address1, useraddress.Address2, useraddress.City, useraddress.State, useraddress.Country, useraddress.Zipcode);
            return new Business.Models.User(userId, user.Username, user.Email, address);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}