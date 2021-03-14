using System;
using System.Net.Mail;

namespace LooseLeaf.Business.Models
{
    public class User : IUser
    {
        public string UserName { get; }

        public string Email { get; }

        public IAddress Address { get; }

        public IWishlist Wishlist => throw new NotImplementedException();

        public User(string userName, string email, IAddress address)
        {
            if (userName is null)
                throw new ArgumentNullException(nameof(userName));

            if (email is null)
                throw new ArgumentNullException(nameof(email));

            if (address is null)
                throw new ArgumentNullException(nameof(address));

            UserName = userName;

            Email = new MailAddress(email).Address;
        }
    }
}