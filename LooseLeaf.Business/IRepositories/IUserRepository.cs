using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LooseLeaf.Business.Models;

namespace LooseLeaf.Business.IRepositories
{
    public interface IUserRepository : IRepository
    {
        // get user by id
        Task<IUser> GetUserAsync(int userid);

        // get list of all users
        Task<IEnumerable<IUser>> GetAllUsersAsync();

        // add a new user
        Task<int> AddUserAsync(INewUser user);

        //list of recommended books for the user

        Task<IEnumerable<IBook>> GetRecommendedBooksAsync(int userid);
    }
}