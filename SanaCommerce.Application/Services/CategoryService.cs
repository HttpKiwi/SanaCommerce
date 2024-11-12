using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SanaCommerce.Application.DTOs.Create;
using SanaCommerce.Application.DTOs.Read;
using SanaCommerce.Application.DTOs.Update;
using SanaCommerce.Application.Interfaces;
using SanaCommerce.Application.Interfaces.Services;
using SanaCommerce.Domain.Entities;

namespace SanaCommerce.Application.Services;

public class CategoryService(IMapper mapper, IDataContext context) : ICategoryService
{
    public async Task CreateCategoryAsync(CategoryCreateDto categoryDto)
    {
        var category = mapper.Map<Category>(categoryDto);
        context.Categories.Add(mapper.Map<Category>(category));
        await context.SaveChangesAsync();
    }


    public async Task<List<CategoryReadDto>> GetCategoriesAsync()
    {
        var categories = await context.Categories.ToListAsync();

        return categories.Select(category => new CategoryReadDto
        {
            Id = category.Id,
            Name = category.Name,
        }).ToList();
    }

    public async Task<CategoryReadDto> GetCategoryByIdAsync(int categoryId)
    {
        var category = await context.Categories.FindAsync(categoryId);
        if (category == null) return null;
        return new CategoryReadDto
        {
            Id = category.Id,
            Name = category.Name,
        };
    }

    public async Task UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
    {
        var category = mapper.Map<Category>(categoryUpdateDto);
        var categoryToUpdate = await context.Categories.FindAsync(category.Id);
        if (categoryToUpdate == null) throw new NullReferenceException();
        
        categoryToUpdate.Name = category.Name;
        await context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var categoryToDelete = await context.Categories.FindAsync(id);
        if (categoryToDelete == null) throw new NullReferenceException();
        context.Categories.Remove(categoryToDelete);
        await context.SaveChangesAsync();
    }

    public async Task<List<ProductReadDto>> GetProductsByCategoryIdAsync(int categoryId)
    {
        var products = await context.ProductCategories
            .Where(pc => pc.CategoryId == categoryId)
            .Select(pc => pc.Product)
            .ToListAsync();
        
        return mapper.Map<List<ProductReadDto>>(products);
    }
}