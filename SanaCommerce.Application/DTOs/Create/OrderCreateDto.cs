using SanaCommerce.Domain.Entities;

namespace SanaCommerce.Application.DTOs.Create;

public class OrderCreateDto
{
    public int TotalAmount { get; set; }
    public string Address { get; set; }
    public string CustomerId { get; set; }
    public List<string> ProductsCode { get; set; }
}