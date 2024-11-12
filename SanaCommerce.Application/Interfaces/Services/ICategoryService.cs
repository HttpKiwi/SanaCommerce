using SanaCommerce.Application.DTOs.Create;
using SanaCommerce.Application.DTOs.Read;
using SanaCommerce.Application.DTOs.Update;

namespace SanaCommerce.Application.Interfaces.Services;

public interface ICategoryService
{
    public Task<List<CategoryReadDto>> GetCategoriesAsync();
    public Task<CategoryReadDto> GetCategoryByIdAsync(int categoryId);
    public Task CreateCategoryAsync(CategoryCreateDto createDto);
    public Task UpdateCategoryAsync(CategoryUpdateDto updateDto);
    public Task DeleteCategoryAsync(int id);
    public Task<List<ProductReadDto>> GetProductsByCategoryIdAsync(int categoryId);
}