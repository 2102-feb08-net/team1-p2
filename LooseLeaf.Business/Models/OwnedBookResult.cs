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

        public PhysicalCondition Condition { get; }

        public Availability Availability { get; }

        public int Id { get; }

        public Book Book { get; }

        private OwnedBookResult(Book book, int ownerId, PhysicalCondition condition, Availability availability)
        {
            if (book is null)
                throw new ArgumentNullException(nameof(book));
            if (ownerId <= 0)
                throw new ArgumentException("The ownerId must be greater than 0.", nameof(ownerId));
            if (!Enum.IsDefined(condition))
                throw new ArgumentException($"Value '{condition}' does not exist in PhysicalCondition");
            if (!Enum.IsDefined(availability))
                throw new ArgumentException($"Value '{availability}' does not exist in Availability");

            Book = book;

            OwnerId = ownerId;

            Condition = condition;

            Availability = availability;
        }

        public OwnedBookResult(int id, Book book, int ownerId, PhysicalCondition condition, Availability availability) : this(book, ownerId, condition, availability)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than or equal to one.");

            Id = id;
        }
    }
}