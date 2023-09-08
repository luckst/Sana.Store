namespace Sana.Store.Domain
{
    public class Product: BaseEntity
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AvailableStock { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
