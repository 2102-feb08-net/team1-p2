using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LooseLeaf.Web.DTOs
{
    public class User
    {
        [Required]
        public string AuthId { get; set; }

        public string Username { get; set; }

        [Required]
        public string Email { get; set; }
    }
}