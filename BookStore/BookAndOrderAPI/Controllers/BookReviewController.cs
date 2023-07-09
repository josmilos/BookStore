using BookAndOrderAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAndOrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookReviewController : ControllerBase
    {
        private readonly IBookReviewService _bookReviewService;

        public BookReviewController(IBookReviewService bookReviewService)
        {
            _bookReviewService = bookReviewService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<BookReviewDTO>>>> Get()
        {
            throw new NotImplementedException();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<BookReviewDTO>>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("AddBookReview")]
        public async Task<ActionResult<ServiceResponse<List<BookReviewDTO>>>> AddBookReview()
        {
            throw new NotImplementedException();
        }

        [HttpPatch("UpdateBookReview")]
        public async Task<ActionResult<ServiceResponse<List<BookReviewDTO>>>> UpdateBookReview()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("DeleteBookReview/{id}")]
        public async Task<ActionResult<ServiceResponse<List<BookReviewDTO>>>> DeleteBookReview(int id)
        {
            throw new NotImplementedException();
        }
    }
}
