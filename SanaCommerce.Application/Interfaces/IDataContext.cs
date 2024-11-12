using Microsoft.EntityFrameworkCore;
using SanaCommerce.Domain.Entities;
namespace SanaCommerce.Application.Interfaces;

public interface IDataContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<ProductCategory> ProductCategories { get; set; }
    DbSet<OrderProduct> OrderProducts { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}