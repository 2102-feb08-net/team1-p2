using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LooseLeaf.Business.Models;

namespace LooseLeaf.Business
{
    public interface IUserRepository
    {
            // get user by id
        Task<IUser> GetUser(int userid);
            // get list of all users 
        Task<IEnumerable<IUser>> GetAllUsers();

        // add a new user
        Task AddUser();

        //list of recommended books for the user

        Task<IEnumerable<IBook>> GetRecommendedBooks(int userid);
        



    }
}