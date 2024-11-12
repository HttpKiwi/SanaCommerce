using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SanaCommerce.Application.DTOs.Create;
using SanaCommerce.Application.DTOs.Read;
using SanaCommerce.Application.DTOs.Update;
using SanaCommerce.Application.Interfaces;
using SanaCommerce.Application.Interfaces.Services;
using SanaCommerce.Domain.Entities;

namespace SanaCommerce.Application.Services;

public class ProductService(IMapper mapper, IDataContext context) : IProductService
{
    public async Task<List<ProductReadDto>> GetProductsAsync()
    {
        var products = await context.Products
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .ToListAsync();

        return mapper.Map<List<ProductReadDto>>(products);
    }

    public async Task<ProductReadDto> GetProductByIdAsync(string productCode)
    {
        var product = await context.Products
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.ProductCode == productCode);
        if (product == null) return null;
        return mapper.Map<ProductReadDto>(product);
    }

    public async Task CreateProductAsync(ProductCreateDto createDto)
    {
        var product = mapper.Map<Product>(createDto);
        if (await GetProductByIdAsync(product.ProductCode) != null)
            throw new InvalidOperationException("A product with this code already exists");

        product.ProductCategories = new List<ProductCategory>();

        foreach (var categoryId in createDto.CategoryIds)
        {
            var category = await context.Categories.FindAsync(categoryId);
            if (category == null) throw new NullReferenceException("Category not found");
            var productCategory = new ProductCategory
            {
                Category = category,
                CategoryId = categoryId,
                Product = product,
                ProductCode = product.ProductCode
            };
            product.ProductCategories.Add(productCategory);
        }

        context.Products.Add(product);
        await context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(ProductUpdateDto updateDto)
    {
        var productToUpdate = await context.Products
            .Include(p => p.ProductCategories)
            .FirstOrDefaultAsync(p => p.ProductCode == updateDto.ProductCode);
        if (productToUpdate == null) throw new NullReferenceException("Product not found");


        productToUpdate.Name = updateDto.Name ?? productToUpdate.Name;
        productToUpdate.Description = updateDto.Description ?? productToUpdate.Description;
        productToUpdate.Price = updateDto.Price ?? productToUpdate.Price;
        productToUpdate.Stock = updateDto.Stock ?? productToUpdate.Stock;
        productToUpdate.Photo = updateDto.Photo ?? productToUpdate.Photo;

        if (updateDto.CategoryIds != null && updateDto.CategoryIds.Any())
        {
            foreach (var categoryId in updateDto.CategoryIds)
            {
                var category = await context.Categories.FindAsync(categoryId);
                if (category == null) throw new NullReferenceException("Category not found");
                var productCategory = new ProductCategory
                {
                    Category = category,
                    CategoryId = categoryId,
                    Product = productToUpdate,
                    ProductCode = productToUpdate.ProductCode
                };
                productToUpdate.ProductCategories.Add(productCategory);
            }
        }

        await context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(string productCode)
    {
        var productToDelete = await context.Products.FindAsync(productCode);
        if (productToDelete == null) throw new NullReferenceException("Product not found");
        context.Products.Remove(productToDelete);
        await context.SaveChangesAsync();
    }
}