using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.DataAccess.Repositories
{
    public class LoanRepository
    {
        private readonly LooseLeafContext _context;

        public LoanRepository(LooseLeafContext context)
        {
            _context = context;
        }
    }
}