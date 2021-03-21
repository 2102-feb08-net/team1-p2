using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models.Results
{
    public interface IUserResult
    {
        int Id { get; }
        string Username { get; }
    }
}