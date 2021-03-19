using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public interface IOwnedBook
    {
        int OwnerId { get; }

        PhysicalCondition Condition { get; }

        Availability Availability { get; }

        int Id { get; }

        IIsbnData Isbn { get; }
    }
}