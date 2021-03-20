using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public class Pagination : IPagination
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}