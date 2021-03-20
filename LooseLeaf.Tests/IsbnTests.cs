using LooseLeaf.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LooseLeaf.Tests
{
    public class IsbnTests
    {
        private const long isbn13 = 9780804139038;

        [Theory]
        [InlineData(isbn13)]
        public void IsbnData_Construct_Pass(long isbn)
        {
            // arrange

            // act
            IIsbnData isbnData = new IsbnData(isbn);

            // assert
            Assert.Equal(isbn, isbnData.IsbnValue);
        }

        [Theory]
        [InlineData(97812345678902)]
        [InlineData(978123456789)]
        [InlineData(234567890)]
        [InlineData(-9781234567890)]
        [InlineData(-1234567890)]
        [InlineData(0)]
        public void IsbnData_InvalidIsbn_Fail(long isbn)
        {
            // arrange

            // act
            IIsbnData isbnData() => new IsbnData(isbn);

            // assert
            Assert.Throws<ArgumentException>(isbnData);
        }
    }
}