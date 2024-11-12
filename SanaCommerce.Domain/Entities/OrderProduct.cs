using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanaCommerce.Domain.Entities;

public class OrderProduct
{
    public int OrderId { get; set; }
    public Order Order { get; set; }

    public string ProductCode { get; set; }
    public Product Product { get; set; }
}