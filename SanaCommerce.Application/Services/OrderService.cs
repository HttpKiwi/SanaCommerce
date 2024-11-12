using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SanaCommerce.Application.DTOs.Create;
using SanaCommerce.Application.DTOs.Read;
using SanaCommerce.Application.DTOs.Update;
using SanaCommerce.Application.Interfaces;
using SanaCommerce.Application.Interfaces.Services;
using SanaCommerce.Domain.Entities;

namespace SanaCommerce.Application.Services;

public class OrderService(IMapper mapper, IDataContext context) : IOrderService
{
    public async Task CreateOrderAsync(OrderCreateDto orderDto)
    {
        var customer = await context.Customers.FindAsync(orderDto.CustomerId);

        var order = new Order
        {
            Customer = customer,
            Address = orderDto.Address,
            Created = DateTime.UtcNow,
            TotalAmount = orderDto.TotalAmount,
        };
        
        order.OrderProducts = new List<OrderProduct>();
        
        foreach (var productCode in orderDto.ProductsCode)
        {
            var product = await context.Products.FindAsync(productCode);
            if(product == null) throw new NullReferenceException("Product not found");
            
            product.OrderProducts ??= new List<OrderProduct>();
            
            var orderProduct = new OrderProduct
            {
                Order = order,
                ProductCode = product.ProductCode,
                Product = product,
                OrderId = order.Id
            };
            product.OrderProducts.Add(orderProduct);
        }
        context.Orders.Add(order);
        await context.SaveChangesAsync();
    }

    public async Task<List<OrderReadDto>> GetOrdersAsync()
    {
        var orders = await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .ToListAsync();

        return mapper.Map<List<OrderReadDto>>(orders);
    }

    public async Task<OrderReadDto> GetOrderByIdAsync(int orderId)
    {
        var order = await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);
        
        if (order == null) return null;

        return mapper.Map<OrderReadDto>(order);
    } 
    
    public async Task UpdateOrderAsync(OrderUpdateDto updateDto)
    {
        var orderToUpdate = await context.Orders.FindAsync(updateDto.Id);
        if (orderToUpdate == null) throw new NullReferenceException("Order not found");
        
        orderToUpdate.Address = updateDto.Address ?? orderToUpdate.Address;
        await context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int orderId)
    {
        var orderToDelete = await context.Orders.FindAsync(orderId);
        if (orderToDelete == null) throw new NullReferenceException("Order not found");
        context.Orders.Remove(orderToDelete);
        await context.SaveChangesAsync();
    }

    public async Task<List<ProductReadDto>> GetProductByOrderIdAsync(int categoryId)
    {
        var products = await context.OrderProducts
            .Where(op => op.OrderId == categoryId)
            .Select(op => op.Product)
            .ToListAsync();
        
        return mapper.Map<List<ProductReadDto>>(products);
    }


}