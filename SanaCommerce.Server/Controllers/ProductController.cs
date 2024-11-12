using Microsoft.AspNetCore.Mvc;
using SanaCommerce.Application.DTOs.Create;
using SanaCommerce.Application.DTOs.Update;
using SanaCommerce.Application.Interfaces.Services;

namespace SanaCommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductService productService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        return Ok(await productService.GetProductsAsync());
    }

    [HttpGet("{productCode}")]
    public async Task<IActionResult> GetProductById(string productCode)
    {
        return Ok(await productService.GetProductByIdAsync(productCode));
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto productCreateDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        await productService.CreateProductAsync(productCreateDto);
        
        return CreatedAtAction("GetProductById",new {productCode = productCreateDto.ProductCode}, productCreateDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto productUpdateDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        await productService.UpdateProductAsync(productUpdateDto);
        return NoContent();
    }

    [HttpDelete("{productCode}")]
    public async Task<IActionResult> DeleteProduct(string productCode)
    {
        await productService.DeleteProductAsync(productCode);
        return NoContent();
    }
}