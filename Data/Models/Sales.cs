using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumCafe.Data.Models
{
    public class Sales
    {
        public Guid SalesId { get; set; } = Guid.NewGuid();
        public Guid? MemberId { get; set; } 
        public string Number { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public float OrderTotal { get; set; }
    }
}
