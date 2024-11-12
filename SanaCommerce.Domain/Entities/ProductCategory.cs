using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanaCommerce.Domain.Entities;

public class ProductCategory
{
    public string ProductCode { get; set; }
    public Product Product { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}