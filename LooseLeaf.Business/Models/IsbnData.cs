using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public record IsbnData : IIsbnData
    {
        public long IsbnValue { get; }

        public IsbnData(long isbn)
        {
        }
    }
}