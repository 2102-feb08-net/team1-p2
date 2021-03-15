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
        [HttpGet("api/books")]
        public async Task<IActionResult> GetAllBooks(string genre, string author, string title)
        {
            throw new NotImplementedException();
        }

        [HttpGet("api/books/{bookId}")]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("api/books/{bookId}")]
        public async Task<IActionResult> UpdateBook(int bookId, InterfaceModels.Book book)
        {
            throw new NotImplementedException();
        }
    }
}