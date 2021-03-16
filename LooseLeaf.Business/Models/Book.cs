using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public class Book : IBook
    {
        public int Id { get; }
        public string Title { get; }

        public string Author { get; }

        public ulong Isbn { get; }

        public int Genreid { get; }

        public Book(string title, string author, string isbn, int genreid)
        {
            if (title is null)
                throw new ArgumentNullException(nameof(title));

            if (author is null)
                throw new ArgumentNullException(nameof(author));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException(message: "Book Title cannot be blank.");
             
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException(message: "Book Author cannot be blank.");

            if (isbn is null)
                throw new ArgumentNullException(nameof(isbn));

            Title = title;
            Author = author;
            Isbn = FormatISBN(isbn);
            Genreid = genreid;
        }

        public Book(int id, string title, string author, string isbn, int genreid)
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

            if (isbn is null)
                throw new ArgumentNullException(nameof(isbn));

            Id = id;
            Title = title;
            Author = author;
            Isbn = FormatISBN(isbn);
            Genreid = genreid;
        }

        private static ulong FormatISBN(string isbn)
        {
            const int ISBN_LENGTH_OLD = 10;
            const int ISBN_LENGTH_NEW = 13;

            var formatted = isbn.Replace("-", string.Empty);
            if (ulong.TryParse(formatted, out ulong isbnValue))
            {
                if (isbnValue.ToString().Length is ISBN_LENGTH_OLD or ISBN_LENGTH_NEW)
                    return isbnValue;
                else
                    throw new ArgumentException("An ISBN must be 10 or 13 digits long.");
            }
            else
                throw new ArgumentException("ISBN be a 10 or 13 digit long number with the only allowed seperators being dashes (-).");
        }
    }
}