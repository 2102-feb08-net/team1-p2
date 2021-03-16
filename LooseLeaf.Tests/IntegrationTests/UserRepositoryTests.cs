using LooseLeaf.Business.Models;
using LooseLeaf.DataAccess;
using LooseLeaf.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;

namespace LooseLeaf.Tests.IntegrationTests
{
    public class UserRepositoryTests
    {
        private Mock<IAddress> fakeAddress;

        public UserRepositoryTests()
        {
            ConstructAddress();
        }

        private void ConstructAddress()
        {
            fakeAddress = new Mock<IAddress>();
            fakeAddress.Setup(a => a.Address1).Returns("123 Street");
            fakeAddress.Setup(a => a.City).Returns("Cityville");
            fakeAddress.Setup(a => a.State).Returns("State");
            fakeAddress.Setup(a => a.ZipCode).Returns(123456);
        }

        [Fact]
        public async Task UserRepository_AddUserAsync()
        {
            // arrange
            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext context = contextFactory.CreateContext())
            {
                UserRepository userRepository = new UserRepository(context);
                var fakeUser = new Mock<IUser>();
                fakeUser.Setup(u => u.Address).Returns(fakeAddress.Object);
                fakeUser.Setup(u => u.UserName).Returns("username");
                fakeUser.Setup(u => u.Email).Returns("username@website.com");

                // act
                await userRepository.AddUserAsync(fakeUser.Object);
            }

            // assert
            using var assertContext = contextFactory.CreateContext();
            Assert.True(assertContext.Users.Single() != null);
        }
    }
}