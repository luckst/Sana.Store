namespace Sana.Store.Entities.Models
{
    public class CreateOrderModel
    {
        public Guid CustomerId { get; set; }
        public List<CreateOrderDetailModel> Details { get; set; }
    }
}
