namespace SanaCommerce.Application.DTOs.Read;

public class ProductReadDto
{
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public byte[] Photo { get; set; }
    public List<CategoryReadDto> Categories { get; set; }
}