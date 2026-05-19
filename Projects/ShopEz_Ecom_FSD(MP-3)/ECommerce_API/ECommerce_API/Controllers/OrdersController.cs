using ECommerce_API.DTOs;
using ECommerce_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(OrderDTO dto)
        {
            try
            {
                var order = await _service.CreateOrder(dto);
                return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
        // GET api/orders/myorders
        [Authorize]  // any logged-in user
        [HttpGet("myorders")]
        public async Task<IActionResult> GetMyOrders()
        {
            try
            {
                // Get current user ID from the token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                    return Unauthorized("Invalid user");

                var orders = await _service.GetOrdersByUserId(userId);
                return Ok(orders);
            }
            catch
            {
                return StatusCode(500, "Error fetching orders");
            }
        }
    }
}