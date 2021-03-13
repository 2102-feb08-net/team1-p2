using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public interface IUser
    {
        string UserName { get; }

        string Email { get; }

        IAddress Address { get; }
    }
}