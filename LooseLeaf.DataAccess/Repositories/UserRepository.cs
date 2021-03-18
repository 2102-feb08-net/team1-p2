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
                Userpassword = "NO PASSWORD", //TODO: replaced with hashed password
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
                    u.Username,
                    u.Email,
                    new Business.Models.Address(u.Address.Address1, u.Address.Address2, u.Address.City, u.Address.State, u.Address.Country, u.Address.Zipcode),
                    new Business.Models.Wishlist()
                )).ToListAsync();
            return userList;
        }

        public async Task<IEnumerable<IBook>> GetRecommendedBooksAsync(int userid)
        {
            throw new NotImplementedException();
        }

        public async Task<IUser> GetUserAsync(int userid)
        {
            var user = await _context.Users.Where(b => b.Id == userid).SingleAsync();
            var useraddress = await _context.Addresses.Where(b => b.Id == user.AddressId).SingleAsync();
            Business.Models.Address address = new Business.Models.Address(useraddress.Address1, useraddress.Address2, useraddress.City, useraddress.State, useraddress.Country, useraddress.Zipcode);
            Business.Models.Wishlist userwishlist = new Business.Models.Wishlist();
            return new Business.Models.User(user.Username, user.Email, address, userwishlist);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}