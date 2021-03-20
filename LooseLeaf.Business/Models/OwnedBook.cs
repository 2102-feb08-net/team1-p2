using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public class OwnedBook : IOwnedBook
    {
        public PhysicalCondition Condition { get; }

        public Availability Availability { get; }

        public int Id { get; }

        public IIsbnData Isbn { get; }

        public int OwnerId { get; }

        public OwnedBook(IIsbnData isbn, int ownerId, PhysicalCondition condition, Availability availability)
        {
            if (isbn is null)
                throw new ArgumentNullException(nameof(isbn));
            if (ownerId <= 0)
                throw new ArgumentException("The ownerId must be greater than 0.", nameof(ownerId));
            if (!Enum.IsDefined(condition))
                throw new ArgumentException($"Value '{condition}' does not exist in PhysicalCondition");
            if (!Enum.IsDefined(availability))
                throw new ArgumentException($"Value '{availability}' does not exist in Availability");

            Isbn = isbn;

            OwnerId = ownerId;

            Condition = condition;

            Availability = availability;
        }

        public OwnedBook(int id, IIsbnData isbn, int ownerId, PhysicalCondition condition, Availability availability) : this(isbn, ownerId, condition, availability)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than or equal to one.");

            Id = id;
        }
    }
}