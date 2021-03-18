using LooseLeaf.Business.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LooseLeaf.Web.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _usersRepo;

        public UsersController(IUserRepository usersRepo)
        {
            _usersRepo = usersRepo;
        }

        [HttpGet("api/users")]
        public async Task<IActionResult> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        [HttpPost("api/users")]
        public async Task<IActionResult> AddNewUser(InterfaceModels.User user)
        {
            throw new NotImplementedException();
        }

        [HttpPut("api/users")]
        public async Task<IActionResult> LogUserIn(string username, string password)
        {
            throw new NotImplementedException();
        }

        [HttpGet("api/users/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("api/users/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("api/users/{userId}/books")]
        public async Task<IActionResult> GetBooksByUser(int userId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("api/users/{userId}/books")]
        public async Task<IActionResult> AddUserOwnedBooks(List<InterfaceModels.Book> books)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("api/users/{userId}/books/{bookId}")]
        public async Task<IActionResult> UpdateBookDetails(int userId, int bookId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("api/users/{userId}/wishlist")]
        public async Task<IActionResult> GetWishlistByUser(int userId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("api/users/{userId}/wishlist")]
        public async Task<IActionResult> AddBooksToUserWishlist(int userId, List<InterfaceModels.Book> books)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("api/users/{userId}/wishlist")]
        public async Task<IActionResult> DeleteUserWishlist(int userId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("api/users/{userId}/wishlist/{bookId}")]
        public async Task<IActionResult> DeleteBookFromUserWishlist(int userId, int bookId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("api/users/{userId}/loans")]
        public async Task<IActionResult> GetUserLoanHistory(int userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("api/users/{userId}/requests")]
        public async Task<IActionResult> GetUserRequests(int userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("api/users/{userId}/recommendations")]
        public async Task<IActionResult> GetUserRecommendations(int userId)
        {
            throw new NotImplementedException();
        }
    }
}