﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public interface IBook
    {
        int Id { get; }

        string Title { get; }

        string Author { get; }

        long Isbn { get; }

        IEnumerable<string> Genres { get; }

        string Thumbnail { get; }
    }
}