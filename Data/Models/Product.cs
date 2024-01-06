using BisleriumCafe.Data.Enums;

namespace BisleriumCafe.Data.Models;

public class Product
{
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public string ProductName { get; set; }
    public ProductType ProductType { get; set; }
    public float Price { get; set; }
}