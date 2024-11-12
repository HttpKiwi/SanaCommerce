namespace SanaCommerce.Application.DTOs.Read;

public class OrderReadDto
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public int TotalAmount { get; set; }
    public string Address { get; set; }
    public List<string> Products { get; set; } 
    public string CustomerId { get; set; }
}