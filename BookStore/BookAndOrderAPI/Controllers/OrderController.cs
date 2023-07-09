using BookAndOrderAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAndOrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<OrderDTO>>>> Get() 
        {
            throw new NotImplementedException();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<OrderDTO>>> GetById(int id)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost("AddOrder")]
        public async Task<ActionResult<ServiceResponse<List<OrderDTO>>>> AddOrder()
        {
            throw new NotImplementedException();
        }
        
        [HttpPatch("UpdateOrder")]
        public async Task<ActionResult<ServiceResponse<List<OrderDTO>>>> UpdateOrder()
        {
            throw new NotImplementedException();
        }
        
        [HttpDelete("DeleteOrder/{id}")]
        public async Task<ActionResult<ServiceResponse<List<OrderDTO>>>> DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}
