using Microsoft.AspNetCore.Mvc;
using SanaCommerce.Application.DTOs.Create;
using SanaCommerce.Application.DTOs.Update;
using SanaCommerce.Application.Interfaces;
using SanaCommerce.Application.Interfaces.Services;
using SanaCommerce.Domain.Entities;

namespace SanaCommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService categoryService) : Controller
{
    [HttpGet] 
    public async Task<IActionResult> GetCategories()
    {
        return Ok(await categoryService.GetCategoriesAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        return Ok(await categoryService.GetCategoryByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        await categoryService.CreateCategoryAsync(categoryCreateDto);
        
        return CreatedAtAction("GetCategoryById",new {id = categoryCreateDto.Name}, categoryCreateDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDto categoryUpdateDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        await categoryService.UpdateCategoryAsync(categoryUpdateDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/products")]
    public async Task<IActionResult> GetProductsByCategoryId(int id)
    {
        return Ok(await categoryService.GetProductsByCategoryIdAsync(id)); 
    }
}