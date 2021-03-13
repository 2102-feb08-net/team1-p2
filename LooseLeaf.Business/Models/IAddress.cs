using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public interface IAddress
    {
        string Address1 { get; }

        string Address2 { get; }

        string City { get; }

        string State { get; }

        string Country { get; }

        int ZipCode { get; }
    }
}