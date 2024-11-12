using SanaCommerce.Application.DTOs.Create;
using SanaCommerce.Application.DTOs.Read;
using SanaCommerce.Application.DTOs.Update;

namespace SanaCommerce.Application.Interfaces.Services;

public interface IProductService
{
    public Task<List<ProductReadDto>> GetProductsAsync();
    public Task<ProductReadDto> GetProductByIdAsync(string productCode);
    public Task CreateProductAsync(ProductCreateDto createDto);
    public Task UpdateProductAsync(ProductUpdateDto updateDto);
    public Task DeleteProductAsync(string productCode);
}