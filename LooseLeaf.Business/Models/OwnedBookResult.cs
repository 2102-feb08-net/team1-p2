using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public class OwnedBookResult : IOwnedBookResult
    {
        public int OwnerId { get; }

        public string Condition { get; }

        public string Availability { get; }

        public int Id { get; }

        public IBook Book { get; }

        public OwnedBookResult(int id, IBook book, int ownerId, string condition, string availability)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than or equal to one.");

            Id = id;

            if (book is null)
                throw new ArgumentNullException(nameof(book));
            if (ownerId <= 0)
                throw new ArgumentException("The ownerId must be greater than 0.", nameof(ownerId));
            if (string.IsNullOrWhiteSpace(condition))
                throw new ArgumentException($"Value '{condition}' does not exist in PhysicalCondition");
            if (string.IsNullOrWhiteSpace(availability))
                throw new ArgumentException($"Value '{availability}' does not exist in Availability");

            Book = book;

            OwnerId = ownerId;

            Condition = condition;

            Availability = availability;
        }
    }
}