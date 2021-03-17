using LooseLeaf.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.IRepositories
{
    public interface IOwnedBookRepository : IRepository
    {
        Task AddOwnedBookAsync(IOwnedBook ownedBook);

        Task UpdateOwnedBookStatus(int ownedBookId, Availability availability, PhysicalCondition condition);
    }
}