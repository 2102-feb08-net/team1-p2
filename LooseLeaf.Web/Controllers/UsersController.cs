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
        [HttpGet("api/users")]
        public IActionResult GetAllUsers()
        {
            return Ok();
        }

        [HttpPost("api/users")]
        public IActionResult AddNewUser(InterfaceModels.User user)
        {
            return Ok();
        }

        [HttpPut("api/users")]
        public IActionResult LogUserIn(string username, string password)
        {
            return Ok();
        }

        [HttpGet("api/users/{userId}")]
        public IActionResult GetUserById(int userId)
        {
            return Ok();
        }

        [HttpDelete("api/users/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            return Ok();
        }

        [HttpGet("api/users/{userId}/books")]
        public IActionResult GetBooksByUser(int userId)
        {
            return Ok();
        }

        [HttpPost("api/users/{userId}/books")]
        public IActionResult AddUserOwnedBooks(List<InterfaceModels.Book> books)
        {
            return Ok();
        }

        [HttpPatch("api/users/{userId}/books/{bookId}")]
        public IActionResult UpdateBookDetails(int userId, int bookId)
        {
            return Ok();
        }

        [HttpGet("api/users/{userId}/wishlist")]
        public IActionResult GetWishlistByUser(int userId)
        {
            return Ok();
        }

        [HttpPost("api/users/{userId}/wishlist")]
        public IActionResult AddBooksToUserWishlist(int userId, List<InterfaceModels.Book> books)
        {
            return Ok();
        }

        [HttpDelete("api/users/{userId}/wishlist")]
        public IActionResult DeleteUserWishlist(int userId)
        {
            return Ok();
        }

        [HttpDelete("api/users/{userId}/wishlist/{bookId}")]
        public IActionResult DeleteBookFromUserWishlist(int userId, int bookId)
        {
            return Ok();
        }

        [HttpGet("api/users/{userId}/loans")]
        public IActionResult GetUserLoanHistory(int userId)
        {
            return Ok();
        }

        [HttpGet("api/users/{userId}/requests")]
        public IActionResult GetUserRequests(int userId)
        {
            return Ok();
        }

        [HttpGet("api/users/{userId}/recommendations")]
        public IActionResult GetUserRecommendations(int userId)
        {
            return Ok();
        }
    }
}
