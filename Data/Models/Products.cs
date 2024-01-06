using BisleriumCafe.Data.Enums;

namespace BisleriumCafe.Data.Models;

public class Product
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public string HashedPassword { get; set; }
    public Role Role { get; set; }
    public bool HasInitialPassword { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Guid CreatedBy { get; set; }
}