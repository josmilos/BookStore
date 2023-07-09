using BookAndOrderAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAndOrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookOrderController : ControllerBase
    {
        private readonly IBookOrderService _bookOrderService;

        public BookOrderController(IBookOrderService bookOrderService)
        {
            _bookOrderService = bookOrderService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<BookOrderDTO>>>> Get()
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        public async Task<ActionResult<ServiceResponse<BookOrderDTO>>> GetById()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<BookOrderDTO>>>> AddBookOrder()
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public async Task<ActionResult<ServiceResponse<List<BookOrderDTO>>>> UpdateBookOrder()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<BookOrderDTO>>>> DeleteBookOrder()
        {
            throw new NotImplementedException();
        }

    }
}
