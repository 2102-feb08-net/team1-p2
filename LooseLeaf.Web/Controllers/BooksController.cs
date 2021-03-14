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
        public IActionResult GetAllBooks()
        {
            return Ok();
        }

        [HttpGet("api/books/{bookId}")]
        public IActionResult GetBookById(int bookId)
        {
            return Ok();
        }

        [HttpPut("api/books/{bookId}")]
        public IActionResult UpdateBook(int bookId, InterfaceModels.Book book)
        {
            return Ok();
        }

        //not sure how to do the query param one...
        //[HttpGet("api/books")]
        //public IActionResult GetBooksByQueryParams()
        //{
        //    return Ok();
        //}
    }
}
