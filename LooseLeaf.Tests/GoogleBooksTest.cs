using LooseLeaf.Business;
using LooseLeaf.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LooseLeaf.Tests
{
    public class GoogleBooksTest
    {
        [Theory]
        [InlineData(9780804139038, "The Martian")]
        public async Task GoogleBooks_GetBookByIsbn_GetIBook(long isbn, string expectedTitle)
        {
            // arrange
            GoogleBooks googleBooks = new GoogleBooks();

            // act
            IBook data = await googleBooks.GetBookFromIsbn(isbn);

            // assert
            Assert.Equal(expectedTitle, data.Title);
        }
    }
}