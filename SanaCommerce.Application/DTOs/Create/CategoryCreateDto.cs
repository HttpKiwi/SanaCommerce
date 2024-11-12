using System.ComponentModel.DataAnnotations;

namespace SanaCommerce.Application.DTOs.Create;

public class CategoryCreateDto
{
    [Required(ErrorMessage = "Category name is required")]
    public string Name { get; set; }
}