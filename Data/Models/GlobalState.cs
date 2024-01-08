namespace BisleriumCafe.Data.Models
{
    public class GlobalState
    {
        public User CurrentUser { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
