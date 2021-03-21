using System;
using System.Net.Mail;

namespace LooseLeaf.Business.Models
{
    public class User : IUser
    {
        public string UserName { get; }

        public string Email { get; }

        public int Id { get; }

        public User(int userId, string userName, string email)
        {
            if (userId <= 0)
                throw new ArgumentException("The userId must be greater than 0", nameof(userId));

            if (userName is null)
                throw new ArgumentNullException(nameof(userName));

            if (email is null)
                throw new ArgumentNullException(nameof(email));

            UserName = userName;

            Email = new MailAddress(email).Address;

            Id = userId;
        }
    }
}