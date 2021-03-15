using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public record BookSearchParams
    {
        string Genre { get; set; }

        string Author { get; set; }

        string Title { get; set; }
    }
}