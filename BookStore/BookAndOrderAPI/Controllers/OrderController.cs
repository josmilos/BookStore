using Azure.Core;
using BookAndOrderAPI.Services;
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
            var response = await _orderService.GetAllOrders();
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<OrderDTO>>> GetById(int id)
        {
            var response = await _orderService.GetSingleOrder(id);
            if(response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        
        [HttpPost("AddOrder")]
        public async Task<ActionResult<ServiceResponse<List<OrderDTO>>>> AddOrder(OrderDTO request)
        {
            var response = await _orderService.AddOrder(request);
            if (response.Success is false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        
        [HttpPatch("UpdateOrder")]
        public async Task<ActionResult<ServiceResponse<List<OrderDTO>>>> UpdateOrder()
        {
            // TO DO IF NEEDED 
            throw new NotImplementedException();
        }
        
        [HttpDelete("DeleteOrder/{id}")]
        public async Task<ActionResult<ServiceResponse<List<OrderDTO>>>> DeleteOrder(int id)
        {
            var response = await _orderService.DeleteOrder(id);
            if (response.Success is false)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
