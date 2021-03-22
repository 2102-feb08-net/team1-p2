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
        public int LenderId { get; set; }

        [Required]
        public int BorrowerId { get; set; }

        public string Message { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Required]
        public List<int> OwnedBookIds { get; set; }
    }
}