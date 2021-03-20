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

        [Fact]
        public async Task UserRepository_GetUserAsync()
        {
            // arrange
            var insertedUser = new DataAccess.User
            {
                Username = "damionsilver",
                Email = "damion.silver@gmail.com",
                AddressId = 1
            };

            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                await contextFactory.CreateAddress(arrangeContext);
                arrangeContext.SaveChanges();
                await arrangeContext.Users.AddAsync(insertedUser);
                arrangeContext.SaveChanges();
            }

            using var context = contextFactory.CreateContext();
            var repo = new UserRepository(context);

            // act
            IUser user = await repo.GetUserAsync(insertedUser.Id);

            // assert
            Assert.Equal(insertedUser.Username, user.UserName);
            Assert.Equal(insertedUser.Email, user.Email);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        public async Task UserRepository_GetAllUsersAsync(int numberOfUsersToCreate)
        {
            // arrange
            using var contextFactory = new TestLooseLeafContextFactory();
            using (LooseLeafContext arrangeContext = contextFactory.CreateContext())
            {
                for (int i = 0; i < numberOfUsersToCreate; i++)
                    await contextFactory.CreateUser(arrangeContext, $"User {i + 1}");

                arrangeContext.SaveChanges();
            }

            using var context = contextFactory.CreateContext();
            var repo = new UserRepository(context);

            // act
            var users = await repo.GetAllUsersAsync();

            // assert
            Assert.Equal(numberOfUsersToCreate, users.Count());
        }
    }
}