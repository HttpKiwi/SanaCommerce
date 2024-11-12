using Microsoft.AspNetCore.Mvc;
using SanaCommerce.Application.DTOs.Create;
using SanaCommerce.Application.DTOs.Update;
using SanaCommerce.Application.Interfaces.Services;
using SanaCommerce.Application.Services;
using SanaCommerce.Domain.Entities;

namespace SanaCommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(ICustomerService customerService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        return Ok(await customerService.GetCustomersAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(string id)
    {
        return Ok(await customerService.GetCustomerByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDto customerCreateDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        await customerService.CreateCustomerAsync(customerCreateDto);
        
        return CreatedAtAction("GetCustomerById", new { id = customerCreateDto.Id }, customerCreateDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCustomer([FromBody] CustomerUpdateDto customerUpdateDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        await customerService.UpdateCustomerAsync(customerUpdateDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        await customerService.DeleteCustomerAsync(id);
        return NoContent();
    }
}