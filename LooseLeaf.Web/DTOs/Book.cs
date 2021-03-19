using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LooseLeaf.Web.DTOs
{
    public class Book
    {
        [Required]
        public long Isbn { get; }
    }
}