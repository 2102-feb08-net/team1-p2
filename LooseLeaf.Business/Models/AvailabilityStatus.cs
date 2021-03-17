using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public enum AvailabilityStatus
    {
        Available = 1,
        CheckedOut = 2,
        InProcess = 3,
        Unknown = 4,
    }
}