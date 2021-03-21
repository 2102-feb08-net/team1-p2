using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models.Results
{
    public class UserResult : IUserResult
    {
        public int Id { get; }

        public string Username { get; }

        public UserResult(int id, string username)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than 0.");
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty");

            Id = id;
            Username = username;
        }
    }
}