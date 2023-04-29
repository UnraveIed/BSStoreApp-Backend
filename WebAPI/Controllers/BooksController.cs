using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Repositories.EFCore;

namespace WebAPI.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryManager _manager;

        public BooksController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _manager.Book.GetAllBooks(false);
                return Ok(books);
            }
            catch (Exception)
            {

                throw new Exception();
            }
            
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBook([FromRoute(Name = "id")]int id)
        {
            try
            {
                var book = _manager
                    .Book
                    .GetBookById(id, false);

                return Ok(book);
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] Book book)
        {
            try
            {
                _manager.Book.CreateOneBook(book);
                _manager.Save();

                return StatusCode(201, book);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                var entity = _manager
                .Book
                .GetBookById (id, true);

                if (entity is null)
                    return NotFound(); //404

                if (entity.Id != id)
                    return BadRequest(); //400

                entity.Title = book.Title;
                entity.Price = book.Price;

                _manager.Save();

                return Ok(book);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _manager
                    .Book
                    .GetBookById(id, false);

                if (entity is null) 
                    return NotFound();

                _manager.Book.Delete(entity);
                _manager.Save();
                
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        // Duzeltilmeli
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                var entity = _manager
                    .Book
                    .GetBookById(id, true);

                if (entity is null) return NotFound();

                bookPatch.ApplyTo(entity);
                _manager.Book.Update(entity);
                //_manager.Save();

                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
