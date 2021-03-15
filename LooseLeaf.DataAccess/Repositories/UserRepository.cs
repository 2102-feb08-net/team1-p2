using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LooseLeaf.Business;
namespace LooseLeaf.DataAccess.Repositories
{

    public class UserRepository : IUserRepository
    {
        public Task AddUser()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUser>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Business.Models.IBook>> GetRecommendedBooks(int userid)
        {
            throw new NotImplementedException();
        }

        public Task<IUser> GetUser(int userid)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<IUser>> IUserRepository.GetAllUsers()
        {
            throw new NotImplementedException();
        }

        Task<IUser> IUserRepository.GetUser(int userid)
        {
            throw new NotImplementedException();
        }
    }
}