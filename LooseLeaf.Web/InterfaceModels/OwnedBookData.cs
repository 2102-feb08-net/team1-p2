using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LooseLeaf.Business.Models;

namespace LooseLeaf.Web.InterfaceModels
{
    public class OwnedBookData : IValidatableObject
    {
        public int BookId { get; }

        public Availability? AvailabilityStatus { get; }

        public PhysicalCondition? ConditionStatus { get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            if (!AvailabilityStatus.HasValue && !ConditionStatus.HasValue)
                yield return new ValidationResult("Both AvailabilityStatus and ConditionStatus cannot be null");
        }
    }
}