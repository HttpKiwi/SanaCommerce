namespace SanaCommerce.Application.DTOs.Update;

public class ProductUpdateDto
{
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public int? Price { get; set; }
    public string Description { get; set; }
    public int? Stock { get; set; }
    public byte[] Photo { get; set; }
    public List<int> CategoryIds { get; set; }

}