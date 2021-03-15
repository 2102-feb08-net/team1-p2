using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LooseLeaf.Business.Models;

namespace LooseLeaf.Tests
{
    public class AddressTests
    {
        [Fact]
        public void Address_Construct_Success()
        {
            // arrange

            // act
            Address address = new Address("123 Street", "Apt 2", "Newcity", "Newzona", "Country", 12345);

            // assert
            Assert.NotNull(address);
        }

        [Fact]
        public void Address_ConstructNoAddress2_Success()
        {
            // arrange

            // act
            Address address = new Address("123 Street", null, "Newcity", "Newzona", "Country", 12345);

            // assert
            Assert.NotNull(address);
        }

        [Fact]
        public void Address_ConstructNoCountry_Success()
        {
            // arrange

            // act
            Address address = new Address("123 Street", "Apt 2", "Newcity", "Newzona", null, 12345);

            // assert
            Assert.NotNull(address);
        }

        [Fact]
        public void Address_ConstructNoStreet1_Fail()
        {
            // arrange

            // act
            static Address address() => new Address("   ", "Apt 2", "Newcity", "Newzona", "Country", 12345);

            // assert
            Assert.Throws<ArgumentException>(address);
        }

        [Fact]
        public void Address_ConstructNoCity_Fail()
        {
            // arrange

            // act
            static Address address() => new Address("123 Address", "Apt 2", "   ", "Newzona", "Country", 12345);

            // assert
            Assert.Throws<ArgumentException>(address);
        }

        [Fact]
        public void Address_ConstructNoState_Fail()
        {
            // arrange

            // act
            static Address address() => new Address("123 Address", "Apt 2", "Newcity", "   ", "Country", 12345);

            // assert
            Assert.Throws<ArgumentException>(address);
        }

        [Fact]
        public void Address_ConstructNullStreet1_Fail()
        {
            // arrange

            // act
            static Address address() => new Address(null, "Apt 2", "Newcity", "Newzona", "Country", 12345);

            // assert
            Assert.Throws<ArgumentNullException>(address);
        }

        [Fact]
        public void Address_ConstructNullCity_Fail()
        {
            // arrange

            // act
            static Address address() => new Address("123 Address", "Apt 2", null, "Newzona", "Country", 12345);

            // assert
            Assert.Throws<ArgumentNullException>(address);
        }

        [Fact]
        public void Address_ConstructNullState_Fail()
        {
            // arrange

            // act
            static Address address() => new Address("123 Address", "Apt 2", "Newcity", null, "Country", 12345);

            // assert
            Assert.Throws<ArgumentNullException>(address);
        }

        [Fact]
        public void Address_ConstructDefaultZip_Fail()
        {
            // arrange

            // act
            static Address address() => new Address("123 Address", "Apt 2", "Newcity", "Newzona", "Country", default);

            // assert
            Assert.Throws<ArgumentException>(address);
        }
    }
}