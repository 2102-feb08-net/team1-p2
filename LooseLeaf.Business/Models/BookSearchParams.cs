using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public record BookSearchParams : IBookSearchParams
    {
        public string Genre { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
    }
}