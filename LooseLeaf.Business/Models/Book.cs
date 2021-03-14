using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public class Book : IBook
    {
        public string Title { get; }

        public string Author { get; }

        public ulong ISBN { get; }

        public DateTime PublishedDate { get; }

        public Book(string title, string author, string isbn, DateTime publishedDate)
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
            ISBN = FormatISBN(isbn);
            PublishedDate = publishedDate;
        }

        private ulong FormatISBN(string isbn)
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