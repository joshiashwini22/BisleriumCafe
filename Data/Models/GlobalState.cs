namespace BisleriumCafe.Data.Models
{
    /* The class GlobalState contains attributes that store 
     * User information in Current user and
     * List of OrderItems which is initialized with an Empty List */
    public class GlobalState
    {
        public User CurrentUser { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
