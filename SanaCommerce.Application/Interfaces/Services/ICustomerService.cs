using SanaCommerce.Application.DTOs.Create;
using SanaCommerce.Application.DTOs.Read;
using SanaCommerce.Application.DTOs.Update;

namespace SanaCommerce.Application.Interfaces.Services;

public interface ICustomerService
{
    public Task<List<CustomerReadDto>> GetCustomersAsync();
    public Task<CustomerReadDto> GetCustomerByIdAsync(string customerId);
    public Task CreateCustomerAsync(CustomerCreateDto createDto);
    public Task UpdateCustomerAsync(CustomerUpdateDto updateDto);
    public Task DeleteCustomerAsync(string id);
    public Task AddOrderAsync(OrderCreateDto orderCreateDto);
}