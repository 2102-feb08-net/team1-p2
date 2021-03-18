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
        [Range(1, int.MaxValue)]
        public int Id { get; }
    }
}