using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sana.Store.Domain
{
    public class Order: BaseEntity
    {
        public int Number { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
