using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommerce_API.Services;
using ECommerce_API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce_API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        // CREATE ORDER
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(OrderDTO dto)
        {
            var result = await _service.CreateOrder(dto);
            if (result != "Order Created Successfully")
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET ALL ORDERS
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var orders = await _service.GetAllOrders();
                return Ok(orders);
            }
            catch
            {
                return StatusCode(500, "Error fetching orders");
            }
        }

        // GET ORDER BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var order = await _service.GetOrderById(id);

                if (order == null)
                    return NotFound("Order not found");

                return Ok(order);
            }
            catch
            {
                return StatusCode(500, "Error fetching order");
            }
        }
    }
}
