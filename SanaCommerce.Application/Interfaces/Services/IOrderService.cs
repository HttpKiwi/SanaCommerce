using SanaCommerce.Application.DTOs.Create;
using SanaCommerce.Application.DTOs.Read;
using SanaCommerce.Application.DTOs.Update;

namespace SanaCommerce.Application.Interfaces.Services;

public interface IOrderService
{
    public Task<List<OrderReadDto>> GetOrdersAsync();
    public Task<OrderReadDto> GetOrderByIdAsync(int orderId);
    public Task CreateOrderAsync(OrderCreateDto createDto);
    public Task UpdateOrderAsync(OrderUpdateDto updateDto);
    public Task DeleteOrderAsync(int orderId);
    public Task<List<ProductReadDto>> GetProductByOrderIdAsync(int categoryId);
}