using System.Data;
using LibraryApi.Communication.Requests;
using LibraryApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
  List<Book> books = new List<Book>
  {
    new Book {
      Id = "book01",
      Title = "The Dark Face of Love",
      Author = "Guinever Beck",
      Genre = Types.IGenre.Romance,
      Price = 39.99,
      StorageQuantity = 5
  },
   new Book {
      Id = "book02",
      Title = "The Three Musketeers",
      Author = "Alexandre Dumas",
      Genre = Types.IGenre.Adventure,
      Price = 50.00,
      StorageQuantity = 12
  }
  };

  [HttpGet]
  [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
  public IActionResult FetchAllBooks() => Ok(books);

  [HttpGet]
  [Route("{id}")]
  [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public IActionResult GetSomeBook([FromRoute] string id)
  {
    var book = books.Find((book) => book.Id == id);
    if (book == null)
      return NotFound();

    return Ok(book);
  }

  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public IActionResult CreateBook([FromBody] RequestCreateBook request)
  {
    var newBook = new Book
    {
      Id = Guid.NewGuid().ToString(),
      Title = request.Title,
      Author = request.Author,
      Genre = request.Genre,
      Price = request.Price,
      StorageQuantity = request.StorageQuantity
    };

    bool bookAlreadyExist = books.Any(book => book.Title == request.Title);

    if (bookAlreadyExist)
      return BadRequest("This book already exists.");

    books.Add(newBook);

    return Created(string.Empty, newBook);
  }

  [HttpPut]
  [Route("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public IActionResult UpdateSomeBook([FromRoute] string id, [FromBody] RequestUpdateBook request)
  {
    var bookToUpdate = books.Find((book) => book.Id == id);

      if (bookToUpdate == null)
        return NotFound(); 
    
    bookToUpdate.Title = request.Title;
    bookToUpdate.Author = request.Author;
    bookToUpdate.Genre = request.Genre;
    bookToUpdate.Price = request.Price;
    bookToUpdate.StorageQuantity = request.StorageQuantity;

    return NoContent();
  }

  [HttpDelete]
  [Route("{id}")]
  public IActionResult RemoveSomeBook([FromRoute] string id)
  {
    var book = books.Find((book) => book.Id == id);

    if (book == null)
      return NotFound();

    books.Remove(book);

    return Ok(books);
  }


}