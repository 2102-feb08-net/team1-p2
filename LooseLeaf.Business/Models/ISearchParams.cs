using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public interface ISearchParams
    {
        IPagination Pagination { get; set; }
    }
}