using BookStore.EFLib.Models;
using Microsoft.AspNetCore.Mvc;
using storeapp.Dtos;
using storeapp.Model;
using storeapp.Services;
using System.Reflection.Metadata.Ecma335;

namespace storeapp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService) 
        {
            _bookService = bookService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return Ok(await _bookService.GetAllBookAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookModel>> GetBook(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(new BookModel
            {
                Id = book.BookId,
                Title = book.Title,
                AuthorId = book.AuthorId,
                Price = book.Price,
                PublishedDate = book.PublishedDate,
                Stock = book.Stock
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookModel book)
        {
            await _bookService.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new {id = book.Title}, book);
        }

        [HttpPut("{id}")]
        //public async Task<ActionResult<BookModel>> UpdateBook(int id)
        //{
        //    var book = await _bookService.GetBookById(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(new BookModel
        //    {
        //        Id = book.BookId,
        //        Title = book.Title,
        //        AuthorId = book.AuthorId,
        //        Price = book.Price,
        //        PublishedDate = book.PublishedDate,
        //        Stock = book.Stock
        //    });
        //}


        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            await _bookService.UpdateBookAsync(id, book);
            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }


        
    }
}
