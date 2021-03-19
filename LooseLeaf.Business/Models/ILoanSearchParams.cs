using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public interface ILoanSearchParams
    {
        int CustomerId { get; set; }
        int BookId { get; set; }

        LoanStatus LoanStatus { get; set; }

        Availability BookAvailability { get; set; }
    }
}