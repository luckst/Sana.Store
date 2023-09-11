namespace Sana.Store.Entities.Models
{
    public class CreateOrderDetailModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
