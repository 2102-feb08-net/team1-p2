using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.Business.Models
{
    public class OwnedBook : IOwnedBook
    {
        public IBook Book { get; }

        public IUser Owner { get; }

        public PhysicalCondition Condition { get; }

        public Availability Availability { get; }

        public OwnedBook(IBook book, IUser owner, PhysicalCondition condition, Availability availability)
        {
            if (book is null)
                throw new ArgumentNullException(nameof(book));
            if (owner is null)
                throw new ArgumentNullException(nameof(owner));
            if (!Enum.IsDefined(condition))
                throw new ArgumentException($"Value '{condition}' does not exist in PhysicalCondition");
            if (!Enum.IsDefined(availability))
                throw new ArgumentException($"Value '{condition}' does not exist in Availability");

            Book = book;

            Owner = owner;

            Condition = condition;

            Availability = availability;
        }
    }
}