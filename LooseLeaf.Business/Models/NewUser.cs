using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public class NewUser : INewUser
    {
        public string AuthId { get; }

        public string Username { get; }

        public string Email { get; }

        public NewUser(string authId, string username, string email)
        {
            AuthId = authId;
            Username = username;
            Email = email;
        }
    }
}