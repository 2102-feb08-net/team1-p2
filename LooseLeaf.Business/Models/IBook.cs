using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public interface IBook
    {
        string Title { get; }

        string Author { get; }

        ulong ISBN { get; }

        DateTime PublishedDate { get; }
    }
}