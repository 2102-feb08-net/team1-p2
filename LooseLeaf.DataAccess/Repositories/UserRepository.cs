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
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IBook>> GetRecommendedBooksAsync(int userid)
        {
            throw new NotImplementedException();
        }

        public async Task<IUser> GetUserAsync(int userid)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}