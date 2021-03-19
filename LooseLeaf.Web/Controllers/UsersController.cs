using LooseLeaf.Business.IRepositories;
using LooseLeaf.Business.Models;
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
        private readonly IWishlistRepository _wishlistRepo;
        private readonly IOwnedBookRepository _ownedBookRepo;

        public UsersController(IUserRepository usersRepo, IWishlistRepository wishlistRepo, IOwnedBookRepository ownedBookRepo)
        {
            _usersRepo = usersRepo;
            _wishlistRepo = wishlistRepo;
            _ownedBookRepo = ownedBookRepo;
        }

        [HttpGet("api/users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usersRepo.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("api/users")]
        public async Task<IActionResult> AddNewUser(DTOs.User user)
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
            var user = await _usersRepo.GetUserAsync(userId);
            return Ok(user);
        }

        [HttpGet("api/users/{userId}/books")]
        public async Task<IActionResult> GetBooksByUser(int userId)
        {
            IOwnedBookSearchParams searchParams = new OwnedBookSearchParams() { UserId = userId };
            var ownedBooks = await _ownedBookRepo.GetOwnedBooksAsync(searchParams);
            return Ok(ownedBooks);
        }

        [HttpPost("api/users/{userId}/book")]
        public async Task<IActionResult> AddUserOwnedBook(int userId, DTOs.OwnedBookData data)
        {
            if (!data.ConditionStatus.HasValue)
                return BadRequest();
            if (!data.AvailabilityStatus.HasValue)
                return BadRequest();

            IOwnedBook ownedBookObj = new OwnedBook(new IsbnData(data.Isbn), userId, data.ConditionStatus.Value, data.AvailabilityStatus.Value);
            await _ownedBookRepo.AddOwnedBookAsync(ownedBookObj);
            return Ok();
        }

        [HttpPatch("api/users/{userId}/books/{bookId}")]
        public async Task<IActionResult> UpdateBookDetails(int userId, DTOs.OwnedBookData ownedBook)
        {
            await _ownedBookRepo.UpdateOwnedBookStatus(userId, ownedBook.AvailabilityStatus, ownedBook.ConditionStatus);
            return Ok();
        }

        [HttpGet("api/users/{userId}/wishlist")]
        public async Task<IActionResult> GetWishlistByUser(int userId)
        {
            var wishlist = await _wishlistRepo.GetUserWishlist(userId);
            return Ok(wishlist);
        }

        [HttpPost("api/users/{userId}/wishlist")]
        public async Task<IActionResult> AddBookToUserWishlist(int userId, int bookId)
        {
            await _wishlistRepo.AddBookToUserWishlist(userId, bookId);
            return Ok();
        }

        [HttpDelete("api/users/{userId}/wishlist/{bookId}")]
        public async Task<IActionResult> DeleteBookFromUserWishlist(int userId, int bookId)
        {
            await _wishlistRepo.RemoveBookFromUserWishlist(userId, bookId);
            return Ok();
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