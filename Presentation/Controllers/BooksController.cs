using Entities.DTOs;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {

            var books = _manager.BookService.GetAllBooks(false);
            return Ok(books);

        }

        [HttpGet("{id:int}")]
        public IActionResult GetBook([FromRoute(Name = "id")] int id)
        {

            var book = _manager
                .BookService
                .GetOneBookById(id, false);

            

            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] BookDtoForInsertion bookDto)
        {

            if (bookDto is null)
                return BadRequest();

            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var book = _manager.BookService.CreateOneBook(bookDto);

            // CreatedAtRoute() arastir
            return StatusCode(201, book);

        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
        {

            if (bookDto is null)
                return BadRequest(); // 400

            _manager.BookService.UpdateOneBook(id, bookDto, false);

            return NoContent(); // 204

        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook([FromRoute(Name = "id")] int id)
        {

            _manager.BookService.DeleteOneBook(id, false);

            return NoContent();

        }


        // Duzeltilmeli
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<BookDto> bookPatch)
        {

            var bookDto = _manager
                .BookService
                .GetOneBookById(id, false);

            bookPatch.ApplyTo(bookDto);

            _manager.BookService.UpdateOneBook(id, new BookDtoForUpdate
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                Price = bookDto.Price,
            }, true);

            return NoContent(); // 204
        }

    }
}
