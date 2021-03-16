using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public class Book : IBook
    {
        private const int ISBN_LENGTH_10 = 10;
        private const int ISBN_LENGTH_13 = 13;

        public int Id { get; }
        public string Title { get; }

        public string Author { get; }

        public long Isbn { get; }

        public int GenreId { get; }

        public Book(string title, string author, long isbn, int genreid)
        {
            if (title is null)
                throw new ArgumentNullException(nameof(title));

            if (author is null)
                throw new ArgumentNullException(nameof(author));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException(message: "Book Title cannot be blank.");

            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException(message: "Book Author cannot be blank.");

            int isbnLength = isbn.ToString().Length;
            if (isbn < 0 || (isbnLength != ISBN_LENGTH_10 && isbnLength != ISBN_LENGTH_13))
                throw new ArgumentException("An ISBN number must be 10 or 13 digits long and be non negative.", nameof(isbn));

            Title = title;
            Author = author;
            Isbn = isbn;
            GenreId = genreid;
        }

        public Book(int id, string title, string author, long isbn, int genreid)
        {
            if (id < 1)
                throw new ArgumentException(message: "Book ID cannot be less than 1.");

            if (title is null)
                throw new ArgumentNullException(nameof(title));

            if (author is null)
                throw new ArgumentNullException(nameof(author));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException(message: "Book Title cannot be blank.");

            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException(message: "Book Author cannot be blank.");

            int isbnLength = isbn.ToString().Length;
            if (isbn < 0 || (isbnLength != ISBN_LENGTH_10 && isbnLength != ISBN_LENGTH_13))
                throw new ArgumentException("An ISBN number must be 10 or 13 digits long and be non negative.", nameof(isbn));

            Id = id;
            Title = title;
            Author = author;
            Isbn = isbn;
            GenreId = genreid;
        }
    }
}