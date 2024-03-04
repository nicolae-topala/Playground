using Microsoft.AspNetCore.Mvc;
using WebApp.BLL.Interfaces;
using WebApp.Common.DTOs.Order;

namespace WebApp.API.Controllers
{
    [ApiController]
    [Route("api/Order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("all-orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetOrdersAsync();
            return Ok(result);
        }

        [HttpPost("place-order")]
        public async Task<IActionResult> PostOrder([FromBody] CreateOrderDto orderDto)
        {
            var result = await _orderService.PostOrderAsync(orderDto);
            return Ok(result);
        }
    }
}
