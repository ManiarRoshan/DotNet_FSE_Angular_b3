using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using OrderService.DTOs;
using OrderService.Services;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService) => _orderService = orderService;

        private int GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? User.FindFirst("nameid")?.Value;
            if (string.IsNullOrEmpty(claim))
                throw new UnauthorizedAccessException("User ID claim missing");
            return int.Parse(claim);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDTO dto)
        {
            try
            {
                var order = await _orderService.CreateOrder(dto);
                return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null) return NotFound("Order not found");
            return Ok(order);
        }

        [Authorize]
        [HttpGet("myorders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = GetUserId();
            var orders = await _orderService.GetOrdersByUserId(userId);
            return Ok(orders);
        }

        [Authorize]
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            try
            {
                var userId = GetUserId();
                var isAdmin = User.IsInRole("Admin");
                var order = await _orderService.CancelOrder(id, userId, isAdmin);
                if (order == null) return NotFound("Order not found");
                return Ok(order);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}