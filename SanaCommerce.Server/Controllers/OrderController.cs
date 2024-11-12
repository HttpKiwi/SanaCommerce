using Microsoft.AspNetCore.Mvc;
using SanaCommerce.Application.DTOs.Create;
using SanaCommerce.Application.DTOs.Update;
using SanaCommerce.Application.Interfaces.Services;
using SanaCommerce.Application.Services;
using SanaCommerce.Domain.Entities;

namespace SanaCommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        return Ok(await orderService.GetOrdersAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        return Ok(await orderService.GetOrderByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody] OrderCreateDto orderCreateDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await orderService.CreateOrderAsync(orderCreateDto);

        return CreatedAtAction("GetOrderById", new { id = orderCreateDto.CustomerId }, orderCreateDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateDto orderUpdateDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await orderService.UpdateOrderAsync(orderUpdateDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        await orderService.DeleteOrderAsync(id);
        return NoContent();
    }
}