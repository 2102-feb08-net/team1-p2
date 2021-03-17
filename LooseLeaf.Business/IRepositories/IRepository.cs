using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.IRepositories
{
    public interface IRepository
    {
        Task SaveChangesAsync();
    }
}