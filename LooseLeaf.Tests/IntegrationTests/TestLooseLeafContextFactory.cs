﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LooseLeaf.DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace LooseLeaf.Tests.IntegrationTests
{
    public class TestLooseLeafContextFactory : IDisposable
    {
        private DbConnection _conn;
        private bool _disposedValue;

        private DbContextOptions<LooseLeafContext> CreateOptions() => new DbContextOptionsBuilder<LooseLeafContext>().UseSqlite(_conn).Options;

        public LooseLeafContext CreateContext()
        {
            if (_conn == null)
            {
                _conn = new SqliteConnection("DataSource=:memory:");
                _conn.Open();

                DbContextOptions<LooseLeafContext> options = CreateOptions();
                using var tempContext = new LooseLeafContext(options);
                tempContext.Database.EnsureCreated();
            }

            var context = new LooseLeafContext(CreateOptions());

            CreateEnumData(context);

            return context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _conn.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void CreateEnumData(LooseLeafContext context)
        {
            foreach (string name in Enum.GetNames(typeof(Business.Models.LoanStatus)))
                context.LoanStatuses.Add(new LoanStatus() { StatusName = name });

            foreach (string name in Enum.GetNames(typeof(Business.Models.AvailabilityStatus)))
                context.AvailabilityStatuses.Add(new AvailabilityStatus() { StatusName = name });

            foreach (string name in Enum.GetNames(typeof(Business.Models.PhysicalCondition)))
                context.ConditionStatuses.Add(new ConditionStatus() { StatusName = name });
        }

        public async Task CreateAddress(LooseLeafContext context)
        {
            int id = context.Addresses.Count() + 1;
            await context.Addresses.AddAsync(new DataAccess.Address() { Address1 = $"Street {id}", City = "City", State = "State", Country = "Country", Zipcode = 123456 });
        }

        public async Task CreateUser(LooseLeafContext context, string username = "username")
        {
            if (!context.Addresses.Any())
            {
                await CreateAddress(context);
                await context.SaveChangesAsync();
            }
            await context.Users.AddAsync(new DataAccess.User() { Username = username, Email = $"{username}@website.com", AuthId = $"{username}Id" });
        }

        /// <summary>
        /// Creates a new book along with a genre if one doesn't exist.
        /// </summary>
        /// <param name="context">The DBContext</param>
        /// <param name="bookName">The name of the book</param>
        /// <param name="authorName">The name of the author</param>
        /// <returns>Returns the ISBN of the book.</returns>
        public async Task<long> CreateBook(LooseLeafContext context, string bookName = "Book", string authorName = "Author")
        {
            int id = context.Books.Count() + 1;
            long newIsbn = 9784567890123 + id;
            var book = new DataAccess.Book() { Title = bookName, Author = authorName, Isbn = newIsbn };

            await CreateGenre(context, book);
            await context.Books.AddAsync(book);
            return newIsbn;
        }

        public async Task CreateLoan(LooseLeafContext context, int _LenderId, int _BorrowerId)
        {
            var loan = new DataAccess.Loan()
            {
                LenderId = _LenderId,
                BorrowerId = _BorrowerId,
                Message = "Test loan message",
                LoanStatusId = 1,
                DropoffDate = new DateTime(2021, 3, 20),
                ReturnedDate = new DateTime(2021, 3, 23),
                AddressId = 1
            };
            var loanbooks = new List<LoanedBook> { new LoanedBook() { Loan = loan, OwnedBookid = 1 }, new LoanedBook() { Loan = loan, OwnedBookid = 2 }, new LoanedBook() { Loan = loan, OwnedBookid = 3 } };
            await context.Loans.AddAsync(loan);
            await context.LoanedBooks.AddRangeAsync(loanbooks);
        }

        public async Task CreateGenre(LooseLeafContext context, int bookId, string genre = "Test")
        {
            await context.Genres.AddAsync(new DataAccess.Genre() { GenreName = genre, BookId = bookId });
        }

        public async Task CreateGenre(LooseLeafContext context, Book book, string genre = "Test")
        {
            await context.Genres.AddAsync(new DataAccess.Genre() { GenreName = genre, Book = book });
        }

        public async Task<OwnedBook> CreateOwnedBook(LooseLeafContext context, int userId, int bookId)
        {
            if (!context.Books.Any())
            {
                await CreateBook(context);
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                await CreateUser(context);
                await context.SaveChangesAsync();
            }

            var ownedBook = new DataAccess.OwnedBook() { UserId = userId, BookId = bookId, ConditionId = 1, AvailabilityStatusId = 1 };
            await context.OwnedBooks.AddAsync(ownedBook);
            return ownedBook;
        }
    }
}