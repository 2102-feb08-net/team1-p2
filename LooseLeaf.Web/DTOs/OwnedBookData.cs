using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LooseLeaf.Business.Models;

namespace LooseLeaf.Web.DTOs
{
    public class OwnedBookData : IValidatableObject
    {
        [Required]
        public string Isbn { get; set; }

        public Availability? AvailabilityStatus { get; set; }

        public PhysicalCondition? ConditionStatus { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext ValidationContext)
        {
            if (!AvailabilityStatus.HasValue && !ConditionStatus.HasValue)
                yield return new ValidationResult("Both AvailabilityStatus and ConditionStatus cannot be null");
        }
    }
}