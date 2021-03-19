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
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;

        public BooksController(IBookRepository loanRepo)
        {
            _bookRepo = loanRepo;
        }

        [HttpGet("api/books")]
        public async Task<IActionResult> GetAllBooks(string genre, string author, string title)
        {
            IBookSearchParams searchParams = new BookSearchParams() { Genre = genre, Author = author, Title = title };
            var books = await _bookRepo.GetAllBooks(searchParams);
            return Ok(books);
        }

        [HttpGet("api/books/{bookId}")]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            var book = await _bookRepo.GetBook(bookId);
            return Ok(book);
        }

        [HttpPut("api/books/{bookId}")]
        public async Task<IActionResult> UpdateBook(int bookId, DTOs.Book book)
        {
            throw new NotImplementedException();
        }
    }
}