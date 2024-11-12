using SanaCommerce.Domain.Entities;

namespace SanaCommerce.Application.DTOs.Read;

public class CustomerReadDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public ICollection<OrderReadDto> Orders { get; set; } 
}