using LooseLeaf.Business;
using LooseLeaf.Business.IRepositories;
using LooseLeaf.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private readonly ILoanRepository _loanRepo;

        private readonly GoogleBooks _googleBooks;

        public UsersController(IUserRepository usersRepo, IWishlistRepository wishlistRepo, IOwnedBookRepository ownedBookRepo, ILoanRepository loanRepo, GoogleBooks googleBooks)
        {
            _usersRepo = usersRepo;
            _wishlistRepo = wishlistRepo;
            _ownedBookRepo = ownedBookRepo;
            _loanRepo = loanRepo;
            _googleBooks = googleBooks;
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
            INewUser newUser = new NewUser(user.AuthId, user.Username, user.Email);
            int id = await _usersRepo.AddUserAsync(newUser);
            return Ok(id);
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
        [Authorize]
        public async Task<IActionResult> AddUserOwnedBook(int userId, DTOs.OwnedBookData data)
        {
            var id = User.FindFirst("user_id");
            if (!int.TryParse(id.Value, out int idValue) || idValue != userId)
                return Unauthorized();

            if (!data.ConditionStatus.HasValue)
                return BadRequest();
            if (!data.AvailabilityStatus.HasValue)
                return BadRequest();

            if (!long.TryParse(data.Isbn, out long isbnNumber))
                return BadRequest();

            IOwnedBook ownedBookObj = new OwnedBook(new IsbnData(isbnNumber), userId, data.ConditionStatus.Value, data.AvailabilityStatus.Value);
            await _ownedBookRepo.AddOwnedBookAsync(ownedBookObj, _googleBooks);
            await _ownedBookRepo.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("api/users/{userId}/books/{bookId}")]
        public async Task<IActionResult> UpdateBookDetails(int userId, DTOs.OwnedBookData ownedBook)
        {
            await _ownedBookRepo.UpdateOwnedBookStatus(userId, ownedBook.AvailabilityStatus, ownedBook.ConditionStatus);
            await _ownedBookRepo.SaveChangesAsync();
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
            await _wishlistRepo.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("api/users/{userId}/wishlist/{bookId}")]
        public async Task<IActionResult> DeleteBookFromUserWishlist(int userId, int bookId)
        {
            await _wishlistRepo.RemoveBookFromUserWishlist(userId, bookId);
            await _wishlistRepo.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("api/users/{userId}/loans")]
        [Authorize]
        public async Task<IActionResult> GetUserLoanHistory(int userId)
        {
            ILoanSearchParams searchParams = new LoanSearchParams { BorrowerId = userId, LoanStatus = LoanStatus.Approved };
            var loans = await _loanRepo.GetLoansAsync(searchParams);
            return Ok(loans);
        }

        [HttpGet("api/users/{userId}/requests")]
        [Authorize]
        public async Task<IActionResult> GetUserRequests(int userId)
        {
            ILoanSearchParams searchParams = new LoanSearchParams { LenderId = userId, LoanStatus = LoanStatus.Requested };
            var loans = await _loanRepo.GetLoansAsync(searchParams);
            return Ok(loans);
        }

        [HttpGet("api/users/{userId}/recommendations")]
        [Authorize]
        public async Task<IActionResult> GetUserRecommendations(int userId)
        {
            throw new NotImplementedException();
        }
    }
}