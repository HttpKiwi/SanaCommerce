using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanaCommerce.Domain.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public int TotalAmount { get; set; }
    public string Address { get; set; }
    public Customer Customer { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; } 
}