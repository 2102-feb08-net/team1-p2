using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public record IsbnData : IIsbnData
    {
        public const int ISBN_LENGTH_13 = 13;

        public long IsbnValue { get; }

        public IsbnData(long isbn)
        {
            int isbnLength = isbn.ToString().Length;
            if (isbn < 0 || (isbnLength != ISBN_LENGTH_13))
                throw new ArgumentException("An ISBN number must be 13 digits long and be non negative.", nameof(isbn));

            IsbnValue = isbn;
        }
    }
}