using System;
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

        private int isbnsCreated = 0;

        private DbContextOptions<LooseLeafContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<LooseLeafContext>().UseSqlite(_conn).Options;
        }

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
        }

        public async Task CreateUser(LooseLeafContext context, string username)
        {
            if (context.Addresses.Count() == 0)
            {
                await context.Addresses.AddAsync(new DataAccess.Address() { Address1 = "Street 1", City = "City", State = "State", Zipcode = "123456" });
                await context.SaveChangesAsync();
            }
            await context.Users.AddAsync(new DataAccess.User() { Username = username, Userpassword = "password", Email = $"{username}@website.com", AddressId = 1 });
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Creates a new book along with a genre if one doesn't exist.
        /// </summary>
        /// <param name="context">The DBContext</param>
        /// <param name="bookName">The name of the book</param>
        /// <param name="authorName">The name of the author</param>
        /// <returns>Returns the ISBN of the book.</returns>
        public async Task<long> CreateBook(LooseLeafContext context, string bookName, string authorName)
        {
            if (context.Genres.Count() == 0)
            {
                await context.Genres.AddAsync(new DataAccess.Genre() { GenreName = "Story" });
                await context.SaveChangesAsync();
            }
            long newIsbn = 9784567890123 + isbnsCreated;
            await context.Books.AddAsync(new DataAccess.Book() { Title = bookName, Author = authorName, Isbn = newIsbn, GenreId = 1 });
            await context.SaveChangesAsync();
            isbnsCreated++;
            return newIsbn;
        }

        public async Task CreateOwnedBook(LooseLeafContext context, int userId, int bookId)
        {
            await context.OwnedBooks.AddAsync(new DataAccess.OwnedBook() { UserId = userId, BookId = bookId, Condition = "New", AvailabilityStatusId = 1 });
            await context.SaveChangesAsync();
        }
    }
}