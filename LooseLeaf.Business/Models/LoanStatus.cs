using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public enum LoanStatus
    {
        Requested = 1,
        Approved = 2,
        Denied = 3,
        Expired = 4, // The lender never accepted or denied the loan before it started
    }
}