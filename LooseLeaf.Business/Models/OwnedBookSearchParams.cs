using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public class OwnedBookSearchParams : IOwnedBookSearchParams
    {
        public int? UserId { get; set; }
        public Availability? BookAvailability { get; set; }
        public PhysicalCondition? BookCondition { get; set; }
        public IBookSearchParams BookSearchParams { get; set; }
    }
}