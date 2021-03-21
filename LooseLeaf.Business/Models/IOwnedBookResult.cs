using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public interface IOwnedBookResult
    {
        int OwnerId { get; }

        string Condition { get; }

        string Availability { get; }

        int Id { get; }

        IBook Book { get; }
    }
}