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
                using var context = new LooseLeafContext(options);
                context.Database.EnsureCreated();
            }

            return new LooseLeafContext(CreateOptions());
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
    }
}
