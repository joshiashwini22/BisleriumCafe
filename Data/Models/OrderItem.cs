using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumCafe.Data.Models
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; } = Guid.NewGuid();
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public float ItemPrice { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
    }
}
