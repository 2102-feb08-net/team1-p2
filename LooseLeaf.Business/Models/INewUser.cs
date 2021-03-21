using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public interface INewUser
    {
        string AuthId { get; }

        string Username { get; }

        public string Email { get; }
    }
}