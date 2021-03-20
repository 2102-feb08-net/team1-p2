using LooseLeaf.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LooseLeaf.Web.DTOs
{
    public class LoanRequest
    {
        [Required]
        public int LenderId { get; }

        [Required]
        public int BorrowId { get; }

        public string Message { get; }

        [Required]
        public DateTimeOffset StartDate { get; }

        [Required]
        public DateTimeOffset EndDate { get; }

        [Required]
        public int AddressId { get; }

        [Required]
        public List<int> OwnedBookIds { get; }
    }
}