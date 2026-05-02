using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using UretaLibraryNowAPI.Models;

namespace UretaLibraryNowAPI.Contollers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book 
            {   
                Id = 1,
                Title = "The Hobbit",
                Author = "John Ronald Reuel Tolkien", 
                Genre = "Fantasy",
                Available = true,
                PublishedYear = 1937
            },

            new Book 
            { 
                Id = 2,
                Title = "Project: Hail Mary",
                Author = "Andy Weir",
                Genre = "Science fiction",
                Available = true,
                PublishedYear = 2021
            }

        };
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                status = "success",
                data = books,
                message = "Books Retreived."
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });
            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book retrieved."
            });
        }
        [HttpPost]
        public IActionResult Create([FromBody] Book newbook)
        {
            newbook.Id = books.Count + 1;
            books.Add(newbook);
            return CreatedAtAction(nameof(GetById),
                new { id = newbook.Id },
                new { status = "success",
                    data = newbook,
                    message = "Book Created." });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updatebook)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });
            book.Title = updatebook.Title;
            book.Author = updatebook.Author;
            book.Genre = updatebook.Genre;
            book.Available = updatebook.Available;
            book.PublishedYear  = updatebook.PublishedYear;

            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book updated."
            });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });
            books.Remove(book);
            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book deleted."
            });
        }

    }
}
