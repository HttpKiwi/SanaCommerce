using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SanaCommerce.Domain.Entities;

public class Product
{
    [Key]
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public byte[] Photo { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }

    public ICollection<OrderProduct> OrderProducts { get; set; }
}