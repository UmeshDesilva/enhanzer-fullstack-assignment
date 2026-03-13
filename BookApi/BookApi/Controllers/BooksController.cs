using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>()
        {
            new Book{Id=1, Title="Clean Code", Author="Robert Martin", Isbn="111", PublicationDate=new DateTime(2008,1,1)}
        };

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return books;
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);

            if (book == null)
                return NotFound();

            return book;
        }

        [HttpPost]
        public ActionResult<Book> AddBook(Book book)
        {
            book.Id = books.Max(x => x.Id) + 1;

            books.Add(book);

            return book;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            var book = books.FirstOrDefault(x => x.Id == id);

            if (book == null)
                return NotFound();

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Isbn = updatedBook.Isbn;
            book.PublicationDate = updatedBook.PublicationDate;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);

            if (book == null)
                return NotFound();

            books.Remove(book);

            return NoContent();
        }
    }
}