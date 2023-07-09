using BookAndOrderAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAndOrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<BookDTO>>>> Get()
        {
            var response = await _bookService.GetAllBooks();
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<BookDTO>>> GetById(int id)
        {
            var response = await _bookService.GetSingleBook(id);
            if(response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        

        [HttpPost("AddBook")]
        public async Task<ActionResult<ServiceResponse<List<BookDTO>>>> AddBook(BookDTO request)
        {
            var response = await _bookService.AddBook(request);
            if (response.Success is false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPatch("UpdateBook")]
        public async Task<ActionResult<ServiceResponse<List<BookDTO>>>> UpdateBook(BookDTO request)
        {
            var response = await _bookService.UpdateBook(request);
            if (response.Success is false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteBook/{id}")]
        public async Task<ActionResult<ServiceResponse<List<BookDTO>>>> DeleteBook(int id)
        {
            var response = await _bookService.DeleteBook(id);
            if (response.Success is false)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
