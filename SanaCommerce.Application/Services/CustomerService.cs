using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SanaCommerce.Application.DTOs.Create;
using SanaCommerce.Application.DTOs.Read;
using SanaCommerce.Application.DTOs.Update;
using SanaCommerce.Application.Interfaces;
using SanaCommerce.Application.Interfaces.Services;
using SanaCommerce.Domain.Entities;

namespace SanaCommerce.Application.Services;

public class CustomerService(IMapper mapper, IDataContext context) : ICustomerService
{
    public async Task CreateCustomerAsync(CustomerCreateDto createDto)
    {
        var customer = mapper.Map<Customer>(createDto);
        context.Customers.Add(customer);
        await context.SaveChangesAsync();
    }
    
    public async Task<List<CustomerReadDto>> GetCustomersAsync()
    {
        var customers = await context.Customers
            .Include(c => c.Orders)
            .ToListAsync();
        return mapper.Map<List<CustomerReadDto>>(customers);
    }

    public async Task<CustomerReadDto> GetCustomerByIdAsync(string customerId)
    {
        var customer = await context.Customers
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == customerId);
        return mapper.Map<CustomerReadDto>(customer);
    }


    public async Task UpdateCustomerAsync(CustomerUpdateDto updateDto)
    {
        var customerToUpdate = await context.Customers.FindAsync(updateDto.Id);
        if (customerToUpdate == null) throw new NullReferenceException("Customer not found");
        
        customerToUpdate.Name = updateDto.Name ?? customerToUpdate.Name;
        customerToUpdate.Email = updateDto.Email ?? customerToUpdate.Email;
        customerToUpdate.Phone = updateDto.Phone ?? customerToUpdate.Phone;
        
        await context.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(string id)
    {
        var customerToDelete = await context.Customers.FindAsync(id);
        if (customerToDelete == null) throw new NullReferenceException("Customer not found");
        
        context.Customers.Remove(customerToDelete);
        await context.SaveChangesAsync();
    }

    public async Task AddOrderAsync(OrderCreateDto orderCreateDto)
    {
        throw new NotImplementedException();
    }
}